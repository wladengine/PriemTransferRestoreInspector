﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;

using EducServLib;

namespace PriemForeignInspector
{
    public partial class PersonList : Form
    {
        public PersonList()
        {
            InitializeComponent();
            ExtraInit();
        }

        void ExtraInit()
        {
            LoadStudyLevels();
            LoadSemesters();
            LoadFaculties();
            LoadLicensePrograms();
            LoadObrazPrograms();

            cbFaculty.SelectedValueChanged += new EventHandler(cbFaculty_SelectedValueChanged);
            cbLicenseProgram.SelectedValueChanged += new EventHandler(cbLicenseProgram_SelectedValueChanged);
            cbObrazProgram.SelectedValueChanged += new EventHandler(cbObrazProgram_SelectedValueChanged);
            cbSemester.SelectedIndexChanged += new EventHandler(cbSemester_SelectedIndexChanged);
            cbStudyLevel.SelectedIndexChanged += new EventHandler(cbStudyLevel_SelectedIndexChanged);
        }

        void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? oldSemId = SemesterId;
            LoadSemesters();
            if (oldSemId.HasValue)
                SemesterId = oldSemId;
        }
        void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFaculties();
        }
        void cbObrazProgram_SelectedValueChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        void cbLicenseProgram_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadObrazPrograms();
        }
        void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadLicensePrograms();
        }

        public int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbStudyLevel); }
            set { ComboServ.SetComboId(cbStudyLevel, value); }
        }
        public int? SemesterId
        {
            get { return ComboServ.GetComboIdInt(cbSemester); }
            set { ComboServ.SetComboId(cbSemester, value); }
        }
        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
            set { ComboServ.SetComboId(cbLicenseProgram, value); }
        }
        public int? ObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }

        private void LoadStudyLevels()
        {
            int? OldSL = ComboServ.GetComboIdInt(cbStudyLevel);
            string query = "SELECT DISTINCT StudyLevelId AS Id, StudyLevelName AS Name FROM Entry WHERE SemesterId>1 AND CampaignYear=@CampaignYear ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@CampaignYear", Util.CampaignYear }, { "@DateNow", DateTime.Now } });
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbStudyLevel.AddItems(bind);
            ComboServ.FillCombo(cbStudyLevel, bind, false, true);
        }
        private void LoadSemesters()
        {
            string query = @"SELECT DISTINCT Semester.Id , Semester.Name FROM Semester INNER JOIN Entry ON Entry.SemesterId = Semester.Id 
            WHERE SemesterId>1 AND CampaignYear=@CampaignYear";
            if (StudyLevelId.HasValue)
                query += " AND StudyLevelId=@StudyLevelId";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@CampaignYear", Util.CampaignYear);
            dic.AddVal("@StudyLevelId", StudyLevelId); 
            DataTable tbl = Util.BDC.GetDataTable(query + " ORDER BY 1", dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbSemester.AddItems(bind);
            ComboServ.FillCombo(cbSemester, bind, false, true);
        }
        private void LoadFaculties()
        {
            string query = "SELECT DISTINCT FacultyId AS Id, FacultyName AS Name FROM Entry WHERE CampaignYear=@CampaignYear AND FacultyId IS NOT NULL";
            if (StudyLevelId.HasValue)
                query += " AND StudyLevelId=@StudyLevelId ";
            if (SemesterId.HasValue)
                query += " AND SemesterId=@SemesterId ";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@CampaignYear", Util.CampaignYear);
            dic.AddVal("@StudyLevelId", StudyLevelId);
            dic.AddVal("@SemesterId", SemesterId);
            dic.AddVal("@DateNow", DateTime.Now);
            DataTable tbl = Util.BDC.GetDataTable(query + " ORDER BY 2", dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbFaculty, bind, false, true);
            if (chbFaculty.Checked)
            {
                LoadLicensePrograms();
                cbFaculty.Enabled = true;
            }
            else
            {
                FillGrid();
                cbFaculty.Enabled = false;
            }
        }
        private void LoadLicensePrograms()
        {
            string query = @"SELECT DISTINCT LicenseProgramId AS Id, LicenseProgramCode + ' ' + LicenseProgramName + (case when studylevelid=17 then ' (маг)' else '' end) AS Name 
FROM qEntry WHERE FacultyId=@Id AND CampaignYear=@CampaignYear";
            if (StudyLevelId.HasValue)
                query += " AND StudyLevelId=@StudyLevelId ";
            if (SemesterId.HasValue)
                query += " AND SemesterId=@SemesterId ";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@CampaignYear", Util.CampaignYear);
            dic.AddVal("@StudyLevelId", StudyLevelId);
            dic.AddVal("@SemesterId", SemesterId);
            dic.AddVal("@Id", FacultyId);
            dic.AddVal("@DateNow", DateTime.Now);
            DataTable tblLP = Util.BDC.GetDataTable(query + " ORDER BY 2", dic);
            var bind = (from DataRow rw in tblLP.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbLicenseProgram, bind, false, true);
            if (chbLicenseProgram.Checked)
            {
                LoadObrazPrograms();
                cbLicenseProgram.Enabled = true;
            }
            else
            {
                cbLicenseProgram.Enabled = false;
                FillGrid();
            }
        }
        private void LoadObrazPrograms()
        {
            string query = "SELECT DISTINCT ObrazProgramId AS Id, ObrazProgramCrypt + ' ' + ObrazProgramName AS Name FROM qEntry WHERE LicenseProgramId=@Id AND CampaignYear=@CampaignYear";
            if (StudyLevelId.HasValue)
                query += " AND StudyLevelId=@StudyLevelId ";
            if (SemesterId.HasValue)
                query += " AND SemesterId=@SemesterId ";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.AddVal("@CampaignYear", Util.CampaignYear);
            dic.AddVal("@StudyLevelId", StudyLevelId);
            dic.AddVal("@SemesterId", SemesterId);
            dic.AddVal("@Id", LicenseProgramId);
            DataTable tbl = Util.BDC.GetDataTable(query + " ORDER BY 2", dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbObrazProgram.AddItems(bind);
            ComboServ.FillCombo(cbObrazProgram, bind, false, true);
            FillGrid();
            cbObrazProgram.Enabled = chbObrazProgram.Checked;
        }
        private void FillGrid()
        {
            string query = @"SELECT DISTINCT 
                            extPersonAll.Id as PersonId, 
                            Application.SecondTypeId as AbiturientTypeId, 
                            case when ((select top 1 Application.SecondTypeId from Application where PersonId = extPersonAll.Id and IsCommited = 1)=2) 
								then
									(case 
										when ((Select CountryEducId from PersonEducationDocument where PersonId=extPersonAll.Id)=193)
										then (select [AbiturientType].[Description] from AbiturientType where Id = 3) 
										else (select [AbiturientType].[Description] from AbiturientType where Id = 4) 
										end ) 
								 else 
								 (Select [AbiturientType].[Description] from AbiturientType where AppSecondTypeId = Application.SecondTypeId) end  AS 'Тип', 
                            extPersonAll.Barcode AS ИдНомер, 
                            extPersonAll.Surname + ' ' + extPersonAll.Name + (case when (extPersonAll.SecondName is not null) then (' ' +extPersonAll.SecondName) else ('')end) AS 'ФИО', 
                            Nationality AS Гражданство, 
                            ISNULL(IsDisabled, 0) AS DisabledPerson, 
                            ( select min(Application.DateOfStart) from Application where PersonId = extPersonAll.Id )as 'Дата подачи',
                            CASE WHEN EXISTS(SELECT Id FROM [Application] AS App WHERE App.IsApprovedByComission=1 AND App.PersonId=extPersonAll.Id) THEN 1 ELSE 0 END AS Appr 
                            FROM extPersonAll INNER JOIN [Application] ON [Application].PersonId = extPersonAll.Id 
                            INNER JOIN [qEntry] ON [qEntry].Id = [Application].EntryId 
                            INNER JOIN PersonEducationDocument on PersonEducationDocument.PersonId = extPersonAll.Id
                            INNER JOIN AbiturientType on AbiturientType.AppSecondTypeId = Application.SecondTypeId 
                            WHERE 1=1 AND qEntry.SemesterId > 1 and Application.IsCommited = 1 ";
            
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (SemesterId.HasValue)
            {
                query += " AND qEntry.SemesterId=@SemesterId ";
                dic.AddVal("@SemesterId", SemesterId);
            }
            if (StudyLevelId.HasValue)
            {
                query += " AND qEntry.StudyLevelId=@StudyLevelId ";
                dic.AddVal("@StudyLevelId", StudyLevelId);
            }
            if (chbOnlyLastCampaign.Checked)
                query += " AND Application.DateOfStart >= qEntry.DateOfStart ";//AND qEntry.IsUsedForPriem=1 ";

            if (chbFaculty.Checked && FacultyId.HasValue)
            {
                query += " AND qEntry.FacultyId=@FacId";
                dic.Add("@FacId", FacultyId);
            }
            if (chbLicenseProgram.Checked && LicenseProgramId.HasValue)
            {
                query += " AND qEntry.LicenseProgramId=@LPId";
                dic.Add("@LPId", LicenseProgramId);
            }
            if (chbObrazProgram.Checked && ObrazProgramId.HasValue)
            {
                query += " AND qEntry.ObrazProgramId=@OPId";
                dic.Add("@OPId", ObrazProgramId);
            }
            if (chbShowHidedProfiles.Checked)
            {
                query += " AND (extPersonAll.IsDisabled=@IsDisabled OR extPersonAll.IsDisabled IS NULL)";
                dic.Add("@IsDisabled", false);
            } 
            string sAbitTypes = "";
            if (chbRestore.Checked == false && chbTransfer.Checked == false && chbTransferForeign.Checked == false && chbChangeObrazProgram.Checked == false && chbChangeStudyBasis.Checked == false)
            {
                sAbitTypes = "";
            }
            else
            {
                string ssss = " and ( ";
                ssss += (chbChangeObrazProgram.Checked ? " Application.SecondTypeId = 6 or " : "");
                ssss += (chbRestore.Checked ? " Application.SecondTypeId = 3 or " : "");
                ssss += (chbTransfer.Checked ? " (Application.SecondTypeId = 2 and CountryEducId = 193) or " : "");
                ssss += (chbTransferForeign.Checked ? " (Application.SecondTypeId = 2 and CountryEducId != 193) or " : "");
                ssss += (chbChangeStudyBasis.Checked ? " (Application.SecondTypeId = 5) or " : "");
                ssss = ssss.Substring(0, ssss.Length - 3); 
                sAbitTypes = ssss+")";
            }

            string orderby = @"  /*group by Application.PersonId , Application.SecondTypeId
                                 order by min(Application.DateOfStart)*/";
            DataTable tbl = Util.BDC.GetDataTable(query + sAbitTypes + orderby, dic);
            dgvList.DataSource = tbl;

            dgvList.Columns["AbiturientTypeId"].Visible = false;
            dgvList.Columns["PersonId"].Visible = false;
            //dgvList.Columns["CommitId"].Visible = false;
            dgvList.Columns["DisabledPerson"].Visible = false;
            dgvList.Columns["Appr"].Visible = false;
            
            dgvList.ReadOnly = true;
 
            dgvList.Columns["ФИО"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dgvList.Columns["Место обучения"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            lblCount.Text = tbl.Rows.Count.ToString();
            var FIOs =
                (from DataRow rw in tbl.Rows
                 select rw.Field<string>("ФИО")).ToArray();

            tbFIO.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbFIO.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tbFIO.AutoCompleteCustomSource.Clear();
            tbFIO.AutoCompleteCustomSource.AddRange(FIOs);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Guid id = (Guid)dgvList.Rows[e.RowIndex].Cells["PersonId"].Value;
            int AbiturientTypeId = (int)dgvList.Rows[e.RowIndex].Cells["AbiturientTypeId"].Value;
            Util.OpenPersonCard(this, id, AbiturientTypeId, new UpdateHandler(FillGrid));
        }
        private void chbFaculty_CheckedChanged(object sender, EventArgs e)
        {
            chbLicenseProgram.Checked = false;
            cbFaculty.Enabled = chbFaculty.Checked;
            chbLicenseProgram.Enabled = chbFaculty.Checked;
            if (chbFaculty.Checked)
            {
                LoadFaculties();
            }
            else
            {
                FillGrid();
            }
        }
        private void chbLicenseProgram_CheckedChanged(object sender, EventArgs e)
        {
            chbObrazProgram.Checked = false;
            cbLicenseProgram.Enabled = chbLicenseProgram.Checked;
            chbObrazProgram.Enabled = chbLicenseProgram.Checked;
            if (chbLicenseProgram.Checked)
            {
                LoadLicensePrograms();
            }
            else
            {
                FillGrid();
            }
        }
        private void chbObrazProgram_CheckedChanged(object sender, EventArgs e)
        {
            cbObrazProgram.Enabled = chbObrazProgram.Checked;
            if (chbObrazProgram.Checked)
            {
                LoadObrazPrograms();
            }
            else
            {
                FillGrid();
            }
        }

        private void tbFIO_TextChanged(object sender, EventArgs e)
        {
            dgvList.FindVal("ФИО", tbFIO.Text);
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex >= dgvList.Rows.Count)
                return;
            if (e.ColumnIndex != dgvList.Columns["ФИО"].Index)
                return;

            if ((bool)dgvList.Rows[e.RowIndex].Cells["DisabledPerson"].Value)
                e.CellStyle.BackColor = Color.OrangeRed;
            else if (dgvList.Rows[e.RowIndex].Cells["Appr"].Value.ToString() == "1")
                e.CellStyle.BackColor = Color.GreenYellow;
        }
        private void chbShowHidedProfiles_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void chbOnlyLastCampaign_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void chbTransfer_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void chbTransferForeign_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void chbChangeObrazProgram_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void chbRestore_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Файлы Excel (.xls)|*.xls";
            sfd.FileName = "Список абитуриентов от " + DateTime.Now.ToString("yyyy.mm.dd hh.mm");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application exc = new Excel.Application();
                    Excel.Workbook wb = exc.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet ws = (Excel.Worksheet)exc.ActiveSheet;

                    ws.Name = "Список абитуриентов";
                    int i = 1;
                    int j = 1;

                    foreach (DataGridViewColumn dc in dgvList.Columns)
                    {
                        if (dc.Visible)
                        {
                            ws.Cells[i, j] = dc.HeaderText;
                            j++;
                        }
                    }

                    i++;

                    ProgressForm prog = new ProgressForm(0, dgvList.Rows.Count, 1, ProgressBarStyle.Blocks, "Импорт списка");
                    prog.Show();
                    // печать из грида
                    foreach (DataGridViewRow dr in dgvList.Rows)
                    {
                        j = 1;
                        foreach (DataGridViewColumn dc in dgvList.Columns)
                        {
                            if (dc.Visible)
                            {
                                ws.Cells[i, j] = dr.Cells[dc.Name].Value == null ? "" : "'" + dr.Cells[dc.Name].Value.ToString();
                                j++;
                            }
                        }

                        i++;
                        prog.PerformStep();
                    }
                    prog.Close();

                    wb.SaveAs(sfd.FileName, Excel.XlFileFormat.xlExcel7,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        Excel.XlSaveAsAccessMode.xlExclusive,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value);
                    exc.Visible = true;

                }
                catch (System.Runtime.InteropServices.COMException exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            //На всякий случай
            sfd.Dispose();
        }

        private void chbChangeStudyBasis_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}