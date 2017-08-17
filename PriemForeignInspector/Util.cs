using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Data;
using PriemForeignInspector.EDM;

namespace PriemForeignInspector
{
    static class Util
    {
        public static BDClass BDC { get; private set; }
        public static string TempFilesFolder { get; private set; }
        public static string DirTemplates { get; private set; }
        public static Form MainForm { get; set; }
        public static int CampaignYear { get; private set; }
        public static int CountryRussiaId { get; private set; }

        public static Dictionary<int, string> NationalityList { get; private set; }
        public static Dictionary<int, string> RegionList { get; private set; }
        public static Dictionary<int, string> PassportTypeList { get; private set; }
        public static Dictionary<int, string> StudyLevelList { get; private set; }
        public static Dictionary<int, string> SemesterList { get; private set; }
        public static Dictionary<int, string> StudyFormList { get; private set; }
        public static Dictionary<int, string> StudyBasisList { get; private set; }
        public static Dictionary<int, string> LicenseProgramList { get; private set; }
        public static Dictionary<int, string> ObrazProgramList { get; private set; }
        public static Dictionary<int, string> SchoolTypeList { get; private set; }


        static Util()
        {
            string connStr = "Data Source=SRVPRIEM1;Initial Catalog=OnlinePriem2015;Integrated Security=True;Connect Timeout=300";
            //"Data Source=81.89.183.103;Initial Catalog=OnlinePriem2012;Integrated Security=False;User ID=OnlinePriem2012Inspector;Password=372639BE-888B-4FF4-8D17-0E86B364566C;Connect Timeout=300";
            BDC = new BDClass(connStr);
            TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PriemForeignInspector_TempFiles\";
            DirTemplates = Path.Combine(Application.StartupPath, "Data");
            try
            {
                string sCampaignYear = BDC.GetValue("SELECT [Value] FROM _appsettings WHERE [Key]=@Key", new Dictionary<string, object>() { { "@Key", "PriemYear" } }).ToString();
                int tmp = 0;
                if (!int.TryParse(sCampaignYear, out tmp))
                    CampaignYear = DateTime.Now.Year;
                else
                    CampaignYear = tmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CampaignYear = DateTime.Now.Year;
            }

            CountryRussiaId = 193;
            InitLists();
            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось создать директорию.\r\n" + e.Message);
            }
        }

        public static void ClearTempFolder()
        {
            string[] files = Directory.GetFiles(TempFilesFolder);
            foreach (string filename in files)
            {
                try
                {
                    File.Delete(filename);
                }
                catch { }
            }
        }

        public static void AddVal(this Dictionary<string, object> dic, string Key, object Value)
        {
            if (Value == null)
                Value = DBNull.Value;

            dic.Add(Key, Value);
        }

        public static object ToNullDB(object value)
        {
            if (value == null)
                return DBNull.Value;
            else
                return value;
        }

        public static void AddItems(this ComboBox cb, IList<KeyValuePair<object, string>> bindingSource)
        {
            if (bindingSource.Count == 0)
                return;
            cb.DataSource = bindingSource;
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            cb.PerformLayout();
        }

        public static void OpenAbiturientCard(Form parent, Guid id, int _abitid)
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is AbiturientCard)
                {
                    f.Close();
                }
            }
            var abCard = new AbiturientCard(id, _abitid);
            //parent.AddOwnedForm(abCard);
            abCard.MdiParent = MainForm;
            abCard.Show();
        }
        public static void OpenPersonList()
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is PersonList)
                {
                    f.Close();
                }
            }
            var AbList = new PersonList();
            AbList.MdiParent = MainForm;
            AbList.Show();
        }
        public static void OpenAbitList()
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is AbitList)
                {
                    f.Close();
                }
            }
            var AbList = new AbitList();
            AbList.MdiParent = MainForm;
            AbList.Show();
        } 
        public static void OpenPersonCard(Form parent, Guid id, int AbiturientTypeId )
        {
            OpenPersonCard(parent, id, AbiturientTypeId, null);
        }  
        public static void OpenPersonCard(Form parent, Guid id, int AbiturientTypeId, UpdateHandler handler)
        {
            foreach (Form f in MainForm.MdiChildren)
            {
                if (f is CardPerson)
                {
                    f.Close();
                }
            } 
            switch (AbiturientTypeId)
            {
                case 2:
                    {
                        int? CountryId = (int?)BDC.GetValue("SELECT CountryEducId FROM PersonEducationDocument Inner join Person on Person.Id = PersonId WHERE Person.Id=@Id", new Dictionary<string, object>() { { "@Id", id } });
                        if (CountryId.HasValue)
                            if (CountryId == Util.CountryRussiaId)
                            {
                                var pcard = new CardPersonTransfer(id);
                                pcard._handler = handler;
                                pcard.MdiParent = MainForm;
                                pcard.Show();
                                break;
                            }
                            else
                            {
                                var pcard = new CardPersonTransferForeign(id);
                                pcard._handler = handler;
                                pcard.MdiParent = MainForm;
                                pcard.Show();
                                break;
                            }
                        else { break; }
                    }
                case 3:
                    {
                        var pcard = new CardPersonRestore(id);
                        pcard._handler = handler;
                        pcard.MdiParent = MainForm;
                        pcard.Show();
                        break;
                    }
                case 5:
                    {
                        var pcard = new CardPersonChangeStudyBasis(id);
                        pcard._handler = handler;
                        pcard.MdiParent = MainForm;
                        pcard.Show();
                        break;
                    }
                case 6:
                    {
                        var pcard = new CardPersonChangeObrazProgram(id);
                        pcard._handler = handler;
                        pcard.MdiParent = MainForm;
                        pcard.Show();
                        break;
                    }
            }
        }

        public static string StrConcat(this string[] array)
        {
            string res = "";
            foreach (string s in array)
                res += s;

            return res;
        }

        public static object Id(this ComboBox cb)
        {
            if (cb.SelectedItem == null)
                return null;
            return ((KeyValuePair<object, string>)cb.SelectedItem).Key;
        }
        public static void Id(this ComboBox cb, object val)
        {
            cb.SelectedValue = val;
        }

        public static void Email(string email, string text, string theme = "", string emailFrom = "admission@spbu.ru")
        {
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(_Email, new { To = email, Body = text, Subject = theme, From = emailFrom });
            t.Start();
        }
        private static void _Email(dynamic data)
        {
            string To = data.To;
            string Body = data.Body;
            string Subject = data.Subject;
            string From = data.From;

            try
            { 
                MailerService.Service1Client client = new MailerService.Service1Client();
                bool res = client.Email(To, Body, Subject, "6E764F0D-FFF5-4FA0-A966-05F12091158D", From);
            }
            catch
            {
                MessageBox.Show("Не удалось отправить письмо");
            }
        }

        public static void FindVal(this DataGridView dgv, string searchfield, string pattern)
        {
            int rwIndex = 0;
            foreach (DataGridViewRow rw in dgv.Rows)
            {
                if (rw.Cells[searchfield] != null && rw.Cells[searchfield].Value.ToString().StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
                {
                    //dgv.CurrentCell = dgv.Rows[rwIndex].Cells[searchfield];
                    break;
                }
                rwIndex++;
            }
            if (rwIndex< dgv.Rows.Count)
            {
                dgv.CurrentCell = null;
                dgv.CurrentCell = dgv.Rows[rwIndex].Cells[searchfield];
            }
        }

        public static byte[] GetApplicationPDF(Guid appId, string dirPath)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var abit = (from x in context.Application
                            where x.Id == appId
                            select new
                            {
                                x.PersonId,
                                x.Barcode,
                                Faculty = x.C_Entry.SP_Faculty.Name,
                                Profession = x.C_Entry.SP_LicenseProgram.Name,
                                ProfessionCode = x.C_Entry.SP_LicenseProgram.Code,
                                ObrazProgram = x.C_Entry.SP_ObrazProgram.Name,
                                Specialization = x.C_Entry.SP_Profile.Name,
                                x.C_Entry.StudyFormId,
                                x.C_Entry.StudyForm.Name,
                                x.C_Entry.StudyBasisId,
                                EntryType = (x.C_Entry.StudyLevelId == 17 ? 2 : 1),
                                x.C_Entry.StudyLevelId,
                                x.HostelEduc
                            }).FirstOrDefault();

                string query = "SELECT Email, IsForeign FROM [User] WHERE Id=@Id";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", abit.PersonId } });
                string email = tbl.Rows[0].Field<string>("Email");

                query = "SELECT LanguageNameRus, LevelNameRus FROM extForeignPersonLanguage WHERE PersonId=@PersonId";
                DataTable tblLangs = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@PersonId", abit.PersonId } });
                string Language = string.Join(",", (from DataRow rw in tblLangs.Rows select rw.Field<string>("LanguageNameRus") + " - " + rw.Field<string>("LevelNameRus") + ","));

                var person = (from x in context.Person
                              where x.Id == abit.PersonId
                              select new
                              {
                                  x.Surname,
                                  x.Name,
                                  x.SecondName,
                                  x.PersonAddInfo.HostelAbit,
                                  x.BirthDate,
                                  BirthPlace = x.BirthPlace ?? "",
                                  Sex = x.Sex,
                                  Nationality = x.Nationality.Name,
                                  Country = x.PersonContacts.Country.Name,
                                  PassportType = x.PassportType.Name,
                                  x.PassportSeries,
                                  x.PassportNumber,
                                  x.PassportAuthor,
                                  x.PassportDate,
                                  Address = x.PersonContacts.ForeignAddressInfo,
                                  x.PersonContacts.Phone,
                                  x.PersonContacts.Mobiles,
                                  x.PersonEducationDocument.First().SchoolExitYear,
                                  x.PersonEducationDocument.First().SchoolName,
                                  Language = Language,
                                  AddInfo = x.PersonAddInfo.AddInfo,
                                  Parents = x.PersonAddInfo.Parents,
                                  x.PersonEducationDocument.First().IsEqual,
                                  x.PersonEducationDocument.First().EqualDocumentNumber,
                                  CountryEduc = x.PersonEducationDocument.First().CountryEduc != null ? x.PersonEducationDocument.First().CountryEduc.Name : "",
                                  Qualification = x.PersonEducationDocument.First().PersonHighEducationInfo.Qualification != null ? x.PersonEducationDocument.First().PersonHighEducationInfo.Qualification.Name : "",
                                  x.PersonEducationDocument.First().SchoolTypeId,
                                  EducationDocumentSeries = x.PersonEducationDocument.First().Series,
                                  EducationDocumentNumber = x.PersonEducationDocument.First().Number,
                              }).FirstOrDefault();


                MemoryStream ms = new MemoryStream();
                string dotName;

                if (abit.EntryType == 2)//mag
                    dotName = "MagApplicationForeign.pdf";
                else
                    dotName = "ApplicationForeign.pdf";

                byte[] templateBytes;
                using (FileStream fs = new FileStream(dirPath + dotName, FileMode.Open, FileAccess.Read))
                {
                    templateBytes = new byte[fs.Length];
                    fs.Read(templateBytes, 0, templateBytes.Length);
                }

                PdfReader pdfRd = new PdfReader(templateBytes);
                PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);
                AcroFields acrFlds = pdfStm.AcroFields;
                string code = (3000000 + abit.Barcode).ToString();

                //добавляем штрихкод
                Barcode128 barcode = new Barcode128();
                barcode.Code = code;
                PdfContentByte cb = pdfStm.GetOverContent(1);
                iTextSharp.text.Image img = barcode.CreateImageWithBarcode(cb, null, null);
                if (abit.EntryType == 2)
                    img.SetAbsolutePosition(420, 720);
                else
                    img.SetAbsolutePosition(440, 740);
                cb.AddImage(img);

                acrFlds.SetField("FIO", ((person.Surname ?? "") + " " + (person.Name ?? "") + " " + (person.SecondName ?? "")).Trim());
                acrFlds.SetField("Profession", abit.ProfessionCode + " " + abit.Profession);
                //if (abit.EntryType != 2)
                acrFlds.SetField("Specialization", abit.Specialization);
                acrFlds.SetField("Faculty", abit.Faculty);
                acrFlds.SetField("ObrazProgram", abit.ObrazProgram);
                acrFlds.SetField("StudyForm" + abit.StudyFormId, "1");
                acrFlds.SetField("StudyBasis" + abit.StudyBasisId, "1");

                if (abit.HostelEduc)
                    acrFlds.SetField("HostelEducYes", "1");
                else
                    acrFlds.SetField("HostelEducNo", "1");

                if (person.HostelAbit ?? false)
                    acrFlds.SetField("HostelAbitYes", "1");
                else
                    acrFlds.SetField("HostelAbitNo", "1");

                if (person.Sex)
                    acrFlds.SetField("Male", "1");
                else
                    acrFlds.SetField("Female", "1");

                acrFlds.SetField("BirthDate", person.BirthDate.Value.ToShortDateString());
                acrFlds.SetField("BirthPlace", person.BirthPlace);
                acrFlds.SetField("Nationality", person.Nationality);

                acrFlds.SetField("PassportSeries", person.PassportSeries);
                acrFlds.SetField("PassportNumber", person.PassportNumber);
                acrFlds.SetField("PassportDate", person.PassportDate.Value.ToShortDateString());
                acrFlds.SetField("PassportAuthor", person.PassportAuthor);

                string[] splitStr = GetSplittedStrings(person.Address, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Address" + i.ToString(), splitStr[i - 1]);

                string phones = (person.Phone ?? "") + ", e-mail: " + email + ", " + (person.Mobiles ?? "");
                splitStr = GetSplittedStrings(phones, 30, 70, 2);
                for (int i = 1; i <= 2; i++)
                    acrFlds.SetField("Phone" + i.ToString(), splitStr[i - 1]);

                splitStr = GetSplittedStrings(person.Parents, 40, 70, 3);
                for (int i = 1; i <= 3; i++)
                    acrFlds.SetField("Parents" + i.ToString(), splitStr[i - 1]);

                acrFlds.SetField("ExitYear", person.SchoolExitYear);
                acrFlds.SetField("School", person.SchoolName ?? "");
                //acrFlds.SetField("Original", "0");
                //acrFlds.SetField("Copy", "0");

                acrFlds.SetField("Attestat", person.EducationDocumentSeries + " " + person.EducationDocumentNumber);

                acrFlds.SetField("Language", person.Language ?? "");
                acrFlds.SetField("CountryEduc", person.CountryEduc ?? "");
                acrFlds.SetField("Extra", person.AddInfo ?? "");

                if (person.IsEqual)
                {
                    acrFlds.SetField("HasEqual", "1");
                    acrFlds.SetField("EqualityDocument", person.EqualDocumentNumber);
                }
                else
                    acrFlds.SetField("NoEqual", "1");

                acrFlds.SetField("StudyForm" + abit.StudyFormId, "1");

                if (abit.EntryType != 2)//no mag application
                {
                    if (abit.StudyLevelId == 16)
                        acrFlds.SetField("chbBak", "1");
                    else
                        acrFlds.SetField("chbSpec", "1");

                    if (person.SchoolTypeId != 4)
                        acrFlds.SetField("NoHE", "1");
                    else
                    {
                        acrFlds.SetField("HasHE", "1");
                        acrFlds.SetField("HEName", person.SchoolName);
                    }
                }
                else
                    acrFlds.SetField("Qualification", "1");

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();

                return ms.ToArray();
            }
        }
        public static string[] GetSplittedStrings(string sourceStr, int firstStrLen, int strLen, int numOfStrings)
        {
            sourceStr = sourceStr ?? "";
            string[] retStr = new string[numOfStrings];
            int index = 0, startindex = 0;
            for (int i = 1; i < numOfStrings; i++)
            {
                if (sourceStr.Length > startindex && startindex >= 0)
                {
                    int rowLength = 30;//длина первой строки
                    if (i > 1) //длина остальных строк одинакова
                        rowLength = 70;
                    index = startindex + rowLength;
                    if (index < sourceStr.Length)
                    {
                        index = sourceStr.IndexOf(" ", index);
                        string val = index > 0 ? sourceStr.Substring(startindex, index - startindex) : sourceStr.Substring(startindex);
                        retStr[i] = val;
                    }
                    else
                        retStr[i] = sourceStr.Substring(startindex);
                }
                startindex = index;
            }

            return retStr;
        }
        public static void SetAllControlsEnabled(Control ctrl, bool Enabled)
        {
            bool _isOpen = Enabled;
            if (ctrl is DataGridView)
            {
                ctrl.Enabled = true;
            }
            else if (ctrl is TabControl)
            {
                ctrl.Enabled = true;
                foreach (TabPage tp in ((TabControl)ctrl).TabPages)
                {
                    foreach (Control subCtrl in tp.Controls)
                    {
                        SetAllControlsEnabled(subCtrl, _isOpen);
                    }
                }
            }
            else if (ctrl is GroupBox || ctrl is TabPage)
            {
                ctrl.Enabled = true;
                foreach (Control subCtrl in ctrl.Controls)
                {
                    SetAllControlsEnabled(subCtrl, _isOpen);
                }
            }
            else
                ctrl.Enabled = _isOpen;
        }
        public static void SetAllControlsEnabled(Form frm, bool Enabled)
        {
            bool _isOpen = Enabled;
            foreach (Control ctrl in frm.Controls)
            {
                SetAllControlsEnabled(ctrl, _isOpen);
            }
        }

        public static void InitLists()
        {
            string query = "SELECT Id, ISNULL(Name, NameEng) AS Name FROM Country ORDER BY LevelOfUsing, Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            NationalityList = new Dictionary<int, string>();
            foreach (var x in bind)
                NationalityList.Add((int)x.Key, x.Value);

            query = @"SELECT Region.Id, Region.Name FROM dbo.Region ORDER BY 2 ";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            RegionList = new Dictionary<int, string>();
            foreach (var x in bind)
                RegionList.Add((int)x.Key, x.Value);

            query = "SELECT Id, Name FROM PassportType ORDER BY Name";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            PassportTypeList = new Dictionary<int, string>();
            foreach (var x in bind)
                PassportTypeList.Add((int)x.Key, x.Value);

            query = "SELECT Distinct StudyLevelId AS Id, StudyLevelName AS Name FROM Entry ORDER BY 2";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                    select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            StudyLevelList = new Dictionary<int, string>();
            foreach (var x in bind)
                StudyLevelList.Add((int)x.Key, x.Value);

            query = "SELECT  distinct Semester.Id, Semester.Name FROM Entry join Semester on Entry.SemesterId = Semester.Id ORDER BY 1";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                    select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            SemesterList = new Dictionary<int, string>();
            foreach (var x in bind)
                SemesterList.Add((int)x.Key, x.Value);

            query = "SELECT Id, Name FROM StudyForm ORDER BY 1";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            StudyFormList = new Dictionary<int, string>();
            foreach (var x in bind)
                StudyFormList.Add((int)x.Key, x.Value);

            query = "SELECT Id, Name FROM StudyBasis ORDER BY 1";
            tbl = Util.BDC.GetDataTable(query, null);
            bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            StudyBasisList = new Dictionary<int, string>();
            foreach (var x in bind)
                StudyBasisList.Add((int)x.Key, x.Value);

            query = @"SELECT DISTINCT LicenseProgramId, LicenseProgramCode, LicenseProgramName 
                             FROM Entry   ";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                 orderby rw.Field<string>("LicenseProgramCode")
                 select new KeyValuePair<object, string>(
                     rw.Field<int>("LicenseProgramId"),
                     "(" + rw.Field<string>("LicenseProgramCode") + ") " + rw.Field<string>("LicenseProgramName")
                 )).ToList();
            LicenseProgramList = new Dictionary<int, string>();
            foreach (var x in bind)
                LicenseProgramList.Add((int)x.Key, x.Value);

            query = @"SELECT DISTINCT ObrazProgramId, ObrazProgramName 
                             FROM Entry 
                             ORDER BY ObrazProgramName";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                 orderby rw.Field<string>("ObrazProgramName")
                 select new KeyValuePair<object, string>(
                     rw.Field<int>("ObrazProgramId"),
                     rw.Field<string>("ObrazProgramName")
                 )).ToList();
            ObrazProgramList = new Dictionary<int, string>();
            foreach (var x in bind)
                ObrazProgramList.Add((int)x.Key, x.Value);

            query = "SELECT Id, Name FROM SchoolTypeAll ORDER BY 2";
            tbl = Util.BDC.GetDataTable(query, null);
            bind = (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            SchoolTypeList = new Dictionary<int, string>();
            foreach (var x in bind)
                SchoolTypeList.Add((int)x.Key, x.Value);
        }
    }
}
