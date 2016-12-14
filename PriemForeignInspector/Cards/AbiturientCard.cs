using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace PriemForeignInspector
{
    public delegate void UpdateHandler();
    public partial class AbiturientCard : Form
    {
        private Guid _CommitId;
        private Guid _ApplicationId;
        private int _AbitType;
        private Guid _PersonId;
        private string _FacultyEmail;
        public UpdateHandler UpdateFileGridHandler;
        private bool _sex;
        private bool _isOpen;

        public AbiturientCard(Guid id, int _abitid)
        {
            this.CenterToParent();
            InitializeComponent();
            FillCombos();
            _ApplicationId = id;
            _AbitType = _abitid; 
            FillCard();
            UpdateFileGridHandler += FillCard;
            this.Icon = PriemForeignInspector.Properties.Resources.Application;
            CloseCardForEdit();
        }

        private void FillCombos()
        {
            FillComboStudyLevel();
        }

        private void FillComboStudyLevel()
        {
            string query = "SELECT DISTINCT StudyLevelId AS Id, StudyLevelName AS Name FROM Entry WHERE CampaignYear=@CampaignYear";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@CampaignYear", Util.CampaignYear } });
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbStudyLevel.AddItems(bind);
            ComboServ.FillCombo(cbStudyLevel, bind, false, false);
        }
        private void FillComboSemester()
        {
            string query = "SELECT DISTINCT Semester.Id, Semester.Name FROM Entry INNER JOIN Semester ON Semester.Id = Entry.SemesterId WHERE CampaignYear=@CampaignYear ORDER BY Semester.Id";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@CampaignYear", Util.CampaignYear } });
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbSemester.AddItems(bind);
            ComboServ.FillCombo(cbSemester, bind, false, false);
        }
        private void FillComboLicenseProgram()
        {
            string query = "SELECT DISTINCT LicenseProgramId AS Id, LicenseProgramName AS Name FROM Entry WHERE CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@CampaignYear", Util.CampaignYear);
            if (StudyLevel.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevel);
            }
            if (SemesterId.HasValue)
            {
                query += " AND SemesterId=@SemesterId";
                dic.Add("@SemesterId", SemesterId);
            }
            if (StudyForm.HasValue)
            {
                query += " AND StudyFormId=@StudyFormId";
                dic.Add("@StudyFormId", StudyForm);
            }
            if (StudyBasis.HasValue)
            {
                query += " AND StudyBasisId=@StudyBasisId";
                dic.Add("@StudyBasisId", StudyBasis);
            }
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbLicenseProgram.AddItems(bind);
            ComboServ.FillCombo(cbLicenseProgram, bind, false, false);
        }
        private void FillComboObrazProgram()
        {
            string query = "SELECT DISTINCT ObrazProgramId AS Id, ObrazProgramName AS Name FROM Entry WHERE CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (LicenseProgram.HasValue)
            {
                query += " AND LicenseProgramId=@LicenseProgramId";
                dic.Add("@LicenseProgramId", LicenseProgram);
            }
            if (StudyLevel.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevel);
            }
            if (SemesterId.HasValue)
            {
                query += " AND SemesterId=@SemesterId";
                dic.Add("@SemesterId", SemesterId);
            }
            if (StudyForm.HasValue)
            {
                query += " AND StudyFormId=@StudyFormId";
                dic.Add("@StudyFormId", StudyForm);
            }
            if (StudyBasis.HasValue)
            {
                query += " AND StudyBasisId=@StudyBasisId";
                dic.Add("@StudyBasisId", StudyBasis);
            }
            dic.Add("@CampaignYear", Util.CampaignYear);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbObrazProgram.AddItems(bind);
            ComboServ.FillCombo(cbObrazProgram, bind, false, false);
        }
        private void FillComboSpecialization()
        {
            string query = "SELECT DISTINCT ProfileId AS Id, ProfileName AS Name FROM Entry WHERE CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (ObrazProgram.HasValue)
            {
                query += " AND ObrazProgramId=@ObrazProgramId";
                dic.Add("@ObrazProgramId", ObrazProgram);
            }
            if (LicenseProgram.HasValue)
            {
                query += " AND LicenseProgramId=@LicenseProgramId";
                dic.Add("@LicenseProgramId", LicenseProgram);
            }
            if (StudyLevel.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevel);
            }
            if (SemesterId.HasValue)
            {
                query += " AND SemesterId=@SemesterId";
                dic.Add("@SemesterId", SemesterId);
            }
            if (StudyForm.HasValue)
            {
                query += " AND StudyFormId=@StudyFormId";
                dic.Add("@StudyFormId", StudyForm);
            }
            if (StudyBasis.HasValue)
            {
                query += " AND StudyBasisId=@StudyBasisId";
                dic.Add("@StudyBasisId", StudyBasis);
            }
            dic.Add("@CampaignYear", Util.CampaignYear);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int?>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbProfile.AddItems(bind);
            ComboServ.FillCombo(cbProfile, bind, false, false);
        }
        private void FillComboFaculty()
        {
            string query = "SELECT DISTINCT FacultyId AS Id, SP_Faculty.Name AS Name FROM Entry inner join SP_Faculty on FacultyId=SP_Faculty.Id  WHERE FacultyId IS NOT NULL AND CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (StudyLevel.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevel);
            }
            if (SemesterId.HasValue)
            {
                query += " AND SemesterId=@SemesterId";
                dic.Add("@SemesterId", SemesterId);
            } 
            /*if (Profile != null)
            {
                query += " AND ProfileId=@ProfileId";
                dic.Add("@ProfileId", Profile);
            } */
            if (StudyForm.HasValue)
            {
                query += " AND StudyFormId=@StudyFormId";
                dic.Add("@StudyFormId", StudyForm);
            }
            if (StudyBasis.HasValue)
            {
                query += " AND StudyBasisId=@StudyBasisId";
                dic.Add("@StudyBasisId", StudyBasis);
            }

            if (ObrazProgram.HasValue)
            {
                query += " AND ObrazProgramId=@ObrazProgramId";
                dic.Add("@ObrazProgramId", ObrazProgram);
            }
            if (LicenseProgram.HasValue)
            {
                query += " AND LicenseProgramId=@LicenseProgramId";
                dic.Add("@LicenseProgramId", LicenseProgram);
            }
            dic.Add("@CampaignYear", Util.CampaignYear);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbFaculty.AddItems(bind);
            ComboServ.FillCombo(cbFaculty, bind, false, false);
        }
        private void FillComboStudyForm()
        {
            string query = "SELECT DISTINCT StudyFormId AS Id, StudyFormName AS Name FROM Entry WHERE CampaignYear=@CampaignYear ORDER BY 1";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@CampaignYear", Util.CampaignYear);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbStudyForm.AddItems(bind);
            ComboServ.FillCombo(cbStudyForm, bind, false, false);
        }
        private void FillComboStudyBasis()
        {
            string query = "SELECT DISTINCT StudyBasisId AS Id, StudyBasisName AS Name FROM Entry WHERE CampaignYear=@CampaignYear ORDER BY 1";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@CampaignYear", Util.CampaignYear);
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            //cbStudyBasis.AddItems(bind);
            ComboServ.FillCombo(cbStudyBasis, bind, false, false);
        }

        private void FillCard()
        {
            string query = @"SELECT Surname, Name, SecondName, PersonId, Sex, Entry.LicenseProgramId, Entry.SemesterId, LicenseProgramName,
Entry.ObrazProgramId, ObrazProgramName, Entry.ProfileId, ProfileName, 
 Entry.StudyFormId as StudyFormId, StudyFormName, Entry.FacultyId, FacultyName, Entry.StudyBasisId as StudyBasisId, StudyBasisName, Entry.StudyLevelId, StudyLevelName, 
 case when Enabled = 'True' then 'Подано ' + convert(nvarchar, [Application].DateOfStart, 103) else 'Отозвано ' + convert(nvarchar, DateOfDisable, 103) end AS EnabledInfo,
IsApprovedByComission,
Enabled,
 [User].Email,
Application.CommitId as CommitId
 FROM [Application]
INNER JOIN Entry on Entry.Id = [Application].EntryId 
INNER JOIN Person ON Person.Id = [Application].PersonId 
 INNER JOIN [User] ON [User].Id = Person.Id
 WHERE [Application].Id=@Id  and IsCommited =1 ";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", _ApplicationId } });
            if (tbl.Rows.Count == 0)
                return;
            query = @"select ApplicationCommitVersion.Id, ApplicationCommitVersion.VersionDate from dbo.ApplicationCommitVersion 
join dbo.Application on Application.CommitId = ApplicationCommitVersion.CommitId
                where Application.Id = @Id";
            DataTable tbl_vers = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", _ApplicationId } });

            if (tbl_vers.Rows.Count>0)
                Version = "№ " + tbl_vers.Rows[0].Field<int>("Id").ToString() + " от " + tbl_vers.Rows[0].Field<DateTime>("VersionDate").ToShortDateString();

            DataRow r = tbl.Rows[0];
            _CommitId = r.Field<Guid>("CommitId");
            StudyLevel = r.Field<int>("StudyLevelId");
            SemesterId = r.Field<int>("SemesterId");
            
            StudyForm = r.Field<int>("StudyFormId");
            StudyBasis = r.Field<int>("StudyBasisId");
            FillComboLicenseProgram();
            LicenseProgram = r.Field<int>("LicenseProgramId");
            FillComboObrazProgram();
            ObrazProgram = r.Field<int>("ObrazProgramId");
            FillComboSpecialization();
            int? temp = r.Field<int?>("ProfileId");
            if (temp != null)
            {
                ProfileId = r.Field<int?>("ProfileId"); 
            }
            else
            {
                cbProfile.SelectedIndex = 0;
                
            }  
            FillComboFaculty();         
            Faculty = r.Field<int>("FacultyId");

            FIO = r.Field<string>("Surname") + " " + r.Field<string>("Name") + " " + (r.Field<string>("SecondName")??"");
            Email = r.Field<string>("Email");
            _sex = r.Field<bool>("Sex");
            if (r.Field<bool>("Enabled"))
            {
                tbAppStatus.ForeColor = Color.Green;
                tbAppStatus.Text = r.Field<string>("EnabledInfo");
                if (r.Field<bool>("IsApprovedByComission"))
                {
                    tbAppStatus.Text += " (одобрено)";
                }
            }
            else
            {
                tbAppStatus.ForeColor = Color.Red;
                tbAppStatus.Text = r.Field<string>("EnabledInfo");
            }

            _PersonId = r.Field<Guid>("PersonId");
            _FacultyEmail = "admission@spbu.ru";
            //r.Field<string>("FacultyEmail");
            _sex = r.Field<bool>("Sex");

            Util.BDC.ExecuteQuery("update dbo.Application set IsViewed = 1 where Id=@Id", new Dictionary<string, object>() { { "@Id", _ApplicationId } });

            FillFiles();
        }
        private void FillFiles()
        { 
            string query = @"select 
Id, 
FileName AS 'Имя файла', 
Comment AS 'Комментарий', 
IsReadOnly, 
(case when IsApproved IS NULL then -1 else (case when IsApproved = 'True' then 1 else 0 end )end) AS IsApproved 
FROM extAbitFiles_All 
WHERE PersonId=@PersonId 
AND (ApplicationId is NULL OR ApplicationId=@AppId)";
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "@PersonId", _PersonId },
                { "@AppId", _ApplicationId }
            };
            DataTable tblFiles = Util.BDC.GetDataTable(query, dic);
            dgvFiles.DataSource = tblFiles;
            dgvFiles.Columns["Id"].Visible = false;
            dgvFiles.Columns["IsReadOnly"].Visible = false;
            dgvFiles.Columns["IsApproved"].Visible = false;
            dgvFiles.Columns["Имя файла"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFiles.Columns["Комментарий"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgvFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var value = dgvFiles.Rows[e.RowIndex].Cells["IsApproved"].Value;
            if (value != null && (int)value == 1)
            {
                e.CellStyle.BackColor = Color.Green;
                e.CellStyle.ForeColor = Color.Black;
            }
            else if (value != null && (int)value == 0)
            {
                e.CellStyle.BackColor = Color.Tomato;
                e.CellStyle.ForeColor = Color.Black;
            }
            else
            {
                e.CellStyle.BackColor = Color.LightYellow;
                e.CellStyle.ForeColor = Color.Black;
            }
        }
        private void dgvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Guid id = (Guid)dgvFiles.Rows[e.RowIndex].Cells["Id"].Value;
            string query = "SELECT FileData FROM FileStorage WHERE Id=@Id";
            
            string filename = dgvFiles.Rows[e.RowIndex].Cells["Имя файла"].Value.ToString();

            byte[] unicode_bytes = Encoding.Unicode.GetBytes(filename);
            byte[] ascii_bytes = Encoding.Convert(Encoding.Unicode, Encoding.Default, unicode_bytes);
            string flattened = Encoding.ASCII.GetString(ascii_bytes).Replace('?', '_');

            filename = flattened;

            int lastSlashPos = filename.LastIndexOf('\\');
            if (lastSlashPos > 0)
                filename = filename.Substring(lastSlashPos);

            filename = Util.TemplateFolder + filename;

            byte[] data = (byte[])Util.BDC.GetValue(query, new Dictionary<string, object>() { { "@Id", id } });
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(data);
                    bw.Flush();
                    bw.Close();
                }
                fs.Close();
            }
            System.Diagnostics.Process.Start(filename);
        }
        private void btnIsBad_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выбрано ни одного файла");
                return;
            }
            int rwIndex = dgvFiles.SelectedCells[0].RowIndex;
            string fileinfo = dgvFiles.Rows[rwIndex].Cells["Имя файла"].Value.ToString() + "(" + dgvFiles.Rows[rwIndex].Cells["Комментарий"].Value.ToString() + ")";
            Guid fileId = (Guid)dgvFiles.Rows[rwIndex].Cells["Id"].Value;

            string EmailBody = string.Format(@"Уважаем{7} {0}!
Приёмная комиссия Санкт-Петербургского государственного университета рассмотрела приложенный к заявлению на 
- направление: {1};
- образовательная программа: {2};
- профиль: {3};
- форма обучения: {4};
- основа обучения: {5};

файл: {6}
и была вынуждена отказать в приёме файла.
", FIO, cbLicenseProgram, cbObrazProgram, cbProfile, StudyForm, StudyBasis, fileinfo, _sex ? "ый" : "ая");
            new EmailIsBadFile(EmailBody, Email, _FacultyEmail, fileId, UpdateFileGridHandler).ShowDialog();
        }
        private void btnApproveFile_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выбрано ни одного файла");
                return;
            }
            int rwIndex = dgvFiles.SelectedCells[0].RowIndex;
            string fileinfo = dgvFiles.Rows[rwIndex].Cells["Имя файла"].Value.ToString() + "(" + dgvFiles.Rows[rwIndex].Cells["Комментарий"].Value.ToString() + ")";
            Guid fileId = (Guid)dgvFiles.Rows[rwIndex].Cells["Id"].Value;
            string query = "UPDATE ApplicationFile SET IsApproved='True' WHERE Id=@Id";
            int res = Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@Id", fileId } });
            if (res == 0)
            {
                query = "UPDATE PersonFile SET IsApproved='True' WHERE Id=@Id";
                res = Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@Id", fileId } });
            }
            if (res == 0)
                MessageBox.Show("Не удалось найти файл");

            string EmailBody = string.Format(@"Уважаем{7} {0}!
Приёмная комиссия Санкт-Петербургского государственного университета рассмотрела приложенный к заявлению на 
- направление: {1};
- образовательная программа: {2};
- профиль: {3};
- форма обучения: {4};
- основа обучения: {5};

файл: {6}
и одобрила его.

С уважением,
Приёмная комиссия СПбГУ

----------------------------
Dear {0}!
The Admissions Committee of the St. Petersburg State University, reviewed the file: {6} 
attached to the application on the
- Profession: {1};
- Educational program: {2};
- Profile: {3};
- Study form: {4};

and approved it.

Sincerely,
St. Petersburg State University Admissions Committee
", FIO, cbLicenseProgram, cbObrazProgram, cbProfile, StudyForm, StudyBasis, fileinfo, _sex ? "ая" : "ый");
            Util.Email(Email, EmailBody, "Admissions Committee SPbSU", _FacultyEmail);

            FillFiles();
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выбрано ни одного файла");
                return;
            }
            int rwIndex = dgvFiles.SelectedCells[0].RowIndex;
            Guid id = (Guid)dgvFiles.Rows[rwIndex].Cells["Id"].Value;
            string query = "SELECT FileData FROM FileStorage WHERE Id=@Id";
            string filename = dgvFiles.Rows[rwIndex].Cells["Имя файла"].Value.ToString();

            byte[] unicode_bytes = Encoding.Unicode.GetBytes(filename);
            byte[] ascii_bytes = Encoding.Convert(Encoding.Unicode, Encoding.Default, unicode_bytes);
            string flattened = Encoding.ASCII.GetString(ascii_bytes).Replace('?', '_');

            filename = flattened;

            int lastSlashPos = filename.LastIndexOf('\\');
            if (lastSlashPos > 0)
                filename = filename.Substring(lastSlashPos);

            filename = Util.TemplateFolder + filename;

            byte[] data = (byte[])Util.BDC.GetValue(query, new Dictionary<string, object>() { { "@Id", id } });
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(data);
                    bw.Flush();
                    bw.Close();
                }
                fs.Close();
            }
            System.Diagnostics.Process.Start(filename);
        }
        private void btnEmail_Click(object sender, EventArgs e)
        {
            new EmailForm(_FacultyEmail, Email, _PersonId).Show();
        }
        private void btnPersonCard_Click(object sender, EventArgs e)
        {
            Util.OpenPersonCard(this.Owner, _PersonId, _AbitType);
        }
        private void btnApproveApplication_Click(object sender, EventArgs e)
        {
            string query =
@"SELECT PersonFile.Id, PersonFile.PersonId, 'PF' AS Type 
FROM PersonFile WHERE PersonId=@PersonId AND (IsApproved IS NULL OR IsApproved = 'False')
UNION
SELECT ApplicationFile.Id, Application.PersonId, 'AF' AS Type 
FROM [dbo].[ApplicationFile]
inner join Application on Application.Id = ApplicationFile.ApplicationId or Application.CommitId = ApplicationFile.CommitId
WHERE PersonId=@PersonId AND (IsApproved IS NULL OR IsApproved = 'False') ";
            Dictionary<string, object> dic = new Dictionary<string, object>()
            {
                { "@PersonId", _PersonId } 
            };
            DataTable tblFiles = Util.BDC.GetDataTable(query, dic);
            if (tblFiles.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show(@"Для подтверждения заявления вы должны одобрить все приложенные в портфолио файлы. 
Хотите одобрить файлы автоматически?", "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (DataRow row in tblFiles.Rows)
                    {
                        if (row.Field<string>("Type") == "PF")
                        {
                            query = "UPDATE PersonFile SET IsApproved='True' WHERE Id=@Id";
                            dic.Clear();
                            dic.Add("@Id", row.Field<Guid>("Id"));
                            Util.BDC.ExecuteQuery(query, dic);
                        }
                        else if (row.Field<string>("Type") == "AF")
                        {
                            query = "UPDATE ApplicationFile SET IsApproved='True' WHERE Id=@Id";
                            dic.Clear();
                            dic.Add("@Id", row.Field<Guid>("Id"));
                            Util.BDC.ExecuteQuery(query, dic);
                        }
                    }
                    query = "UPDATE [Application] SET IsApprovedByComission = 'True' WHERE Id=@Id";
                    dic.Clear();
                    dic.Add("@Id", _ApplicationId);
                    Util.BDC.ExecuteQuery(query, dic);
                    MessageBox.Show("OK");
                }
            }
            else
            {
                query = "UPDATE [Application] SET IsApprovedByComission = 'True' WHERE Id=@Id";
                dic.Clear();
                dic.Add("@Id", _ApplicationId);
                Util.BDC.ExecuteQuery(query, dic);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string fileName = Util.TemplateFolder + string.Format("Application_{0}.pdf", Guid.NewGuid().ToString("N"));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                BinaryWriter bw = new BinaryWriter(fs);

                byte[] data = new byte[1024*1024*5];
                /*int? AbTypeId = (int?)Util.BDC.GetValue("SELECT AbiturientTypeId FROM Person INNER JOIN [Application] ON [Application].PersonId=Person.Id WHERE Application.Id=@Id", 
                    new Dictionary<string, object>() { { "@Id", _ApplicationId } });*/
                int? AbTypeId = (int?)Util.BDC.GetValue("SELECT SecondTypeId FROM Application Inner join Person on Person.Id = PersonId and Application.Id = @AppId WHERE Person.Id=@Id", new Dictionary<string, object>() { { "@Id", _PersonId }, { "@AppId", _ApplicationId } });
                switch (AbTypeId)
                {
                    case 2:
                        {
                            int? CountryId = (int?)Util.BDC.GetValue("SELECT CountryEducId FROM PersonEducationDocument Inner join Person on Person.Id = PersonId WHERE Person.Id=@Id", new Dictionary<string, object>() { { "@Id", _PersonId } });
                            if (CountryId.HasValue)
                                if (CountryId == Util.CountryRussiaId)
                                {data = PDFUtils.GetApplicationPDFTransfer(_ApplicationId, Application.StartupPath + "\\Data\\", _PersonId); break;}
                                else
                                {data = PDFUtils.GetApplicationPDFTransferForeign(_ApplicationId, Application.StartupPath + "\\Data\\", _PersonId); break;}
                            else { break; }
                        }
                    case 3:
                        { data = PDFUtils.GetApplicationPDFRecover(_ApplicationId, Application.StartupPath + "\\Data\\", _PersonId); break; }
                    case 5:
                        { data = PDFUtils.GetApplicationPDFChangeStudyBasis(_ApplicationId, Application.StartupPath + "\\Data\\", _PersonId); break; }
                    case 6:
                        { data = PDFUtils.GetApplicationPDFChangeObrazProgram(_ApplicationId, Application.StartupPath + "\\Data\\", _PersonId); break; }
                }
                if (data == null)
                    return;

                bw.Write(data);
                bw.Flush();
                bw.Close();
            }

            System.Diagnostics.Process.Start(fileName);
        }
        
        private void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboSemester();
        }
        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboStudyForm();
        }
        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboObrazProgram();
        }
        private void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboSpecialization();
            FillComboFaculty();
        }
        private void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboFaculty();
        }
        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        { 
        }
        private void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboStudyBasis();
        }
        private void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboLicenseProgram();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isOpen)
            {
                SaveCard();
                CloseCardForEdit();
            }
            else
                OpenCardForEdit();
        }

        private void SaveCard()
        {
            string query = string.Empty;
            query = @"SELECT Id FROM Entry WHERE LicenseProgramId=@LicenseProgramId AND ObrazProgramId=@ObrazProgramId AND FacultyId=@FacultyId 
    AND StudyFormId=@StudyFormId AND StudyBasisId=@StudyBasisId AND StudyLevelId=@StudyLevelId AND SemesterId=@SemesterId AND CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@LicenseProgramId", LicenseProgram);
            dic.Add("@ObrazProgramId", ObrazProgram);
            dic.Add("@FacultyId", Faculty);
            dic.Add("@StudyFormId", StudyForm);
            dic.Add("@StudyBasisId", StudyBasis);
            dic.Add("@StudyLevelId", StudyLevel);
            dic.Add("@SemesterId", SemesterId);
            dic.Add("@CampaignYear", Util.CampaignYear);
            if (ProfileId != null)
            {
                query += " AND ProfileId=@ProfileId";
                dic.Add("@ProfileId", ProfileId);
            }
            Guid? EntryId = (Guid?)Util.BDC.GetValue(query, dic);

            if (_ApplicationId != Guid.Empty)
            {
                query = "UPDATE Application SET EntryId=@EntryId WHERE Id=@Id";
                dic.Clear();
                dic.Add("@EntryId", EntryId); 
                dic.Add("@Id", _ApplicationId);
                Util.BDC.ExecuteQuery(query, dic);
            }
            else
            {
                _ApplicationId = Guid.NewGuid();
                query = @"INSERT INTO Application (Id, PersonId, Enabled, Priority,  EntryId, IsCommited, CommitId) 
            VALUES (@Id, @PersonId, @Enabled, @Priority, @EntryId, @IsCommited, @CommitId)";
                dic.Clear();
                dic.Add("@Id", _ApplicationId);
                dic.Add("@PersonId", _PersonId);
                dic.Add("@Enabled", true);
                dic.Add("@Priority", 0); 
                dic.Add("@EntryId", EntryId);
                dic.Add("@IsCommited", true);
                dic.Add("@CommitId", _CommitId);
                Util.BDC.ExecuteQuery(query, dic);
            }
        }

        private void CloseCardForEdit()
        {
            cbSemester.Enabled = false;
            cbStudyLevel.Enabled = false;
            cbLicenseProgram.Enabled = false;
            cbObrazProgram.Enabled = false;
            cbProfile.Enabled = false;
            cbFaculty.Enabled = false;
            cbStudyForm.Enabled = false;
            cbStudyBasis.Enabled = false;
            _isOpen = false;
            btnSave.Text = "Изменить";
        }
        private void OpenCardForEdit()
        {
            cbSemester.Enabled = true;
            cbStudyLevel.Enabled = true;
            cbLicenseProgram.Enabled = true;
            cbObrazProgram.Enabled = true;
            cbProfile.Enabled = true;
            cbFaculty.Enabled = true;
            cbStudyForm.Enabled = true;
            cbStudyBasis.Enabled = true;
            _isOpen = true;
            btnSave.Text = "Сохранить";
        }

        private void btnChangeIsViewed_Click(object sender, EventArgs e)
        {
            Util.BDC.ExecuteQuery("update dbo.Application set IsViewed = 0 where Id=@Id", new Dictionary<string, object>() { {"@Id",_ApplicationId } });
        }
    }
}
