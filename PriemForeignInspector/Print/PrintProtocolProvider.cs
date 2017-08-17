using Novacode;
using PriemForeignInspector.EDM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace PriemForeignInspector
{
    public interface IPrintProtocolProvider
    {
        DataTable GetProtocolData(int LicenseProgramId, int? StudyFormId, int? StudyBasisId);
        void PrintProtocol(int LicenseProgramId, int? StudyFormId, int? StudyBasisId);
    }

    public abstract class CommonPrintProtocolProvider : IPrintProtocolProvider
    {
        protected abstract string GetTemplate();
        public abstract DataTable GetProtocolData(int LicenseProgramId, int? StudyFormId, int? StudyBasisId);
        public abstract void PrintProtocol(int LicenseProgramId, int? StudyFormId, int? StudyBasisId);
    }

    public class RestorePrintProtocolProvider : CommonPrintProtocolProvider
    {
        protected override string GetTemplate()
        {
            return Path.Combine(Util.DirTemplates, "Protocol_Restore.docx");
        }

        public override DataTable GetProtocolData(int LicenseProgramId, int? StudyFormId, int? StudyBasisId)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("FIO");
            tbl.Columns.Add("Entry");
            tbl.Columns.Add("DisInfo");

            tbl.Columns["FIO"].Caption = "ФИО";
            tbl.Columns["Entry"].Caption = "Заявление";
            tbl.Columns["DisInfo"].Caption = "Инф. о отчислении";

            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var lst = (from App in context.Application
                           join Ent in context.Entry on App.EntryId equals Ent.Id
                           join Sem in context.Semester on Ent.SemesterId equals Sem.Id
                           join Pers in context.Person on App.PersonId equals Pers.Id
                           join Dis in context.PersonDisorderInfo on Pers.Id equals Dis.PersonId into Dis2
                           from Dis in Dis2.DefaultIfEmpty()
                           where Ent.SemesterId > 1
                           && Ent.LicenseProgramId == LicenseProgramId
                           && (StudyFormId.HasValue ? Ent.StudyFormId == StudyFormId : true)
                           && (StudyBasisId.HasValue ? Ent.StudyBasisId == StudyBasisId : true)
                           && Ent.CampaignYear == Util.CampaignYear
                           && Ent.IsUsedForPriem == true
                           && App.IsApprovedByComission == true
                           && App.SecondTypeId == 3 // = Восстановление в СПбГУ
                           select new
                           {
                               Semester = Sem.Name,
                               Course = Sem.EducYear,
                               Ent.LicenseProgramCode,
                               Ent.LicenseProgramName,
                               Ent.ObrazProgramName,
                               Ent.StudyFormName,
                               Ent.StudyBasisName,
                               Pers.Surname,
                               Pers.Name,
                               Pers.SecondName,
                               Dis.EducationProgramName,
                               Dis.YearOfDisorder
                           }).ToList().OrderBy(x => x.Course).ThenBy(x => x.ObrazProgramName).ThenBy(x => x.Surname);

                foreach (var ent in lst)
                {
                    DataRow rw = tbl.NewRow();
                    rw["FIO"] = (((ent.Surname + " ") ?? "") + ((ent.Name + " ") ?? "") + (ent.SecondName ?? "")).Trim();

                    string Entr = "";
                    if (ent.Course.HasValue)
                        Entr += ent.Course + " курс\n";
                    Entr += ent.Semester + "\n";
                    Entr += ent.LicenseProgramCode + " " + ent.LicenseProgramName + "\n";
                    Entr += ent.ObrazProgramName + "\n";
                    Entr += ent.StudyFormName + "\n";
                    Entr += ent.StudyBasisName + "\n";

                    rw["Entry"] = Entr.Trim();

                    string oldEduc = "";
                    if (!string.IsNullOrEmpty(ent.YearOfDisorder))
                        oldEduc += "(" + ent.YearOfDisorder + ") ";
                    if (!string.IsNullOrEmpty(ent.EducationProgramName))
                        oldEduc += ent.EducationProgramName;
                    rw["DisInfo"] = oldEduc;

                    tbl.Rows.Add(rw);
                }
            }

            return tbl;
        }

        public override void PrintProtocol(int LicenseProgramId, int? StudyFormId, int? StudyBasisId)
        {
            using (FileStream fs = new FileStream(GetTemplate(), FileMode.Open, FileAccess.Read))
            using (DocX doc = DocX.Load(fs))
            {
                using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
                {
                    string sLP = "";
                    var LP = context.SP_LicenseProgram.Where(x => x.Id == LicenseProgramId).Select(x => new { x.Code, x.Name }).FirstOrDefault();
                    if (LP != null)
                        sLP = LP.Code + " " + LP.Name;
                    else
                        sLP = "н/д";

                    string sSF = "";
                    string sSB = "";
                    if (StudyFormId.HasValue)
                        sSF = context.StudyForm.Where(x => x.Id == StudyFormId).Select(x => x.Name).FirstOrDefault();
                    else
                        sSF = "все";

                    if (StudyBasisId.HasValue)
                        sSB = context.StudyBasis.Where(x => x.Id == StudyBasisId).Select(x => x.Name).FirstOrDefault();
                    else
                        sSB = "все";

                    doc.ReplaceText("&LicenseProgram&", sLP);
                    doc.ReplaceText("&StudyForm&", sSF);
                    doc.ReplaceText("&StudyBasis&", sSB);
                }

                string sFileName = Path.Combine(Util.DirTemplates, string.Format("Protocol_Restore_{0}.docx", Guid.NewGuid()));
                DataTable tbl = GetProtocolData(LicenseProgramId, StudyFormId, StudyBasisId);

                var td = doc.Tables[0];
                int iBaseRowId = 3;
                var baseRow = td.Rows[iBaseRowId];
                int rwNum = 3;
                int cnt = 0;
                foreach (DataRow rw in tbl.Rows)
                {
                    td.InsertRow(baseRow);
                    rwNum++;

                    td.Rows[rwNum].Cells[0].InsertParagraph((++cnt).ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                    td.Rows[rwNum].Cells[1].InsertParagraph(rw[0].ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                    td.Rows[rwNum].Cells[2].InsertParagraph(rw[1].ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                    td.Rows[rwNum].Cells[3].InsertParagraph(rw[2].ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                }

                td.RemoveRow(iBaseRowId);

                doc.SaveAs(sFileName);
                Process.Start(sFileName);
            }
        }
    }
    public class TransferPrintProtocolProvider : CommonPrintProtocolProvider
    {
        protected override string GetTemplate()
        {
            return Path.Combine(Util.DirTemplates, "Protocol_Transfer.docx");
        }

        public override DataTable GetProtocolData(int LicenseProgramId, int? StudyFormId, int? StudyBasisId)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("FIO");
            tbl.Columns.Add("Entry");
            tbl.Columns.Add("DisInfo");

            tbl.Columns["FIO"].Caption = "ФИО";
            tbl.Columns["Entry"].Caption = "Заявление";
            tbl.Columns["DisInfo"].Caption = "Инф. о отчислении";

            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var lst = (from App in context.Application
                           join Ent in context.Entry on App.EntryId equals Ent.Id
                           join Sem in context.Semester on Ent.SemesterId equals Sem.Id
                           join Pers in context.Person on App.PersonId equals Pers.Id
                           join Dis in context.PersonDisorderInfo on Pers.Id equals Dis.PersonId into Dis2
                           from Dis in Dis2.DefaultIfEmpty()
                           where Ent.SemesterId > 1
                           && Ent.LicenseProgramId == LicenseProgramId
                           && (StudyFormId.HasValue ? Ent.StudyFormId == StudyFormId : true)
                           && (StudyBasisId.HasValue ? Ent.StudyBasisId == StudyBasisId : true)
                           && Ent.CampaignYear == Util.CampaignYear
                           && Ent.IsUsedForPriem == true
                           && App.IsApprovedByComission == true
                           && App.SecondTypeId == 3 // = Восстановление в СПбГУ
                           select new
                           {
                               Semester = Sem.Name,
                               Course = Sem.EducYear,
                               Ent.LicenseProgramCode,
                               Ent.LicenseProgramName,
                               Ent.ObrazProgramName,
                               Ent.StudyFormName,
                               Ent.StudyBasisName,
                               Pers.Surname,
                               Pers.Name,
                               Pers.SecondName,
                               Dis.EducationProgramName,
                               Dis.YearOfDisorder
                           }).ToList().OrderBy(x => x.Course).ThenBy(x => x.ObrazProgramName).ThenBy(x => x.Surname);

                foreach (var ent in lst)
                {
                    DataRow rw = tbl.NewRow();
                    rw["FIO"] = (((ent.Surname + " ") ?? "") + ((ent.Name + " ") ?? "") + (ent.SecondName ?? "")).Trim();

                    string Entr = "";
                    if (ent.Course.HasValue)
                        Entr += ent.Course + " курс\n";
                    Entr += ent.Semester + "\n";
                    Entr += ent.LicenseProgramCode + " " + ent.LicenseProgramName + "\n";
                    Entr += ent.ObrazProgramName + "\n";
                    Entr += ent.StudyFormName + "\n";
                    Entr += ent.StudyBasisName + "\n";

                    rw["Entry"] = Entr.Trim();

                    string oldEduc = "";
                    if (!string.IsNullOrEmpty(ent.YearOfDisorder))
                        oldEduc += "(" + ent.YearOfDisorder + ") ";
                    if (!string.IsNullOrEmpty(ent.EducationProgramName))
                        oldEduc += ent.EducationProgramName;
                    rw["DisInfo"] = oldEduc;

                    tbl.Rows.Add(rw);
                }
            }

            return tbl;
        }

        public override void PrintProtocol(int LicenseProgramId, int? StudyFormId, int? StudyBasisId)
        {
            using (FileStream fs = new FileStream(GetTemplate(), FileMode.Open, FileAccess.Read))
            using (DocX doc = DocX.Load(fs))
            {
                using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
                {
                    string sLP = "";
                    var LP = context.SP_LicenseProgram.Where(x => x.Id == LicenseProgramId).Select(x => new { x.Code, x.Name }).FirstOrDefault();
                    if (LP != null)
                        sLP = LP.Code + " " + LP.Name;
                    else
                        sLP = "н/д";

                    string sSF = "";
                    string sSB = "";
                    if (StudyFormId.HasValue)
                        sSF = context.StudyForm.Where(x => x.Id == StudyFormId).Select(x => x.Name).FirstOrDefault();
                    else
                        sSF = "все";

                    if (StudyBasisId.HasValue)
                        sSB = context.StudyBasis.Where(x => x.Id == StudyBasisId).Select(x => x.Name).FirstOrDefault();
                    else
                        sSB = "все";

                    doc.ReplaceText("&LicenseProgram&", sLP);
                    doc.ReplaceText("&StudyForm&", sSF);
                    doc.ReplaceText("&StudyBasis&", sSB);
                }

                string sFileName = Path.Combine(Util.DirTemplates, string.Format("Protocol_Restore_{0}.docx", Guid.NewGuid()));
                DataTable tbl = GetProtocolData(LicenseProgramId, StudyFormId, StudyBasisId);

                var td = doc.Tables[0];
                int iBaseRowId = 3;
                var baseRow = td.Rows[iBaseRowId];
                int rwNum = 3;
                int cnt = 0;
                foreach (DataRow rw in tbl.Rows)
                {
                    td.InsertRow(baseRow);
                    rwNum++;

                    td.Rows[rwNum].Cells[0].InsertParagraph((++cnt).ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                    td.Rows[rwNum].Cells[1].InsertParagraph(rw[0].ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                    td.Rows[rwNum].Cells[2].InsertParagraph(rw[1].ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                    td.Rows[rwNum].Cells[3].InsertParagraph(rw[2].ToString(), false, new Formatting() { Size = 12, FontFamily = new System.Drawing.FontFamily("Times New Roman") });
                }

                td.RemoveRow(iBaseRowId);

                doc.SaveAs(sFileName);
                Process.Start(sFileName);
            }
        }
    }
}
