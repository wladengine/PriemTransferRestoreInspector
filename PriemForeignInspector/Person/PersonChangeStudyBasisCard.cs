using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriemForeignInspector
{
    public partial class PersonChangeStudyBasisCard : Form
    {
        private Guid _PersonId;
        private bool _isOpen;
        public UpdateHandler _handler;
        bool bFillcombos = false;
        private int _AbitType = 5;

        public PersonChangeStudyBasisCard(Guid id)
        {
            this.Text = "Перевод с платной основы обучения на бюджетную (смена основы обучения)";
            this.CenterToParent();
            InitializeComponent();
            _PersonId = id;
            FillCombos();
            FillCard();
            this.Icon = PriemForeignInspector.Properties.Resources.Person_Male_Light;
            _isOpen = false;
        }

        private void FillCombos()
        {
            FillComboPassportType();
            FillComboNationality();
            FillComboCountry();
            FillComboRegion();
            FillComboStudyLevel(); 
        }

        private void FillComboPassportType()
        {
            string query = "SELECT Id, Name FROM PassportType ORDER BY Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbPassportType.AddItems(bind);
        }
        private void FillComboNationality()
        {
            string query = "SELECT Id, ISNULL(Name, NameEng) AS Name FROM Country ORDER BY LevelOfUsing, Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbNationality.AddItems(bind);
        }
        private void FillComboCountry()
        {
            string query = "SELECT Id, ISNULL(Name, NameEng) AS Name FROM Country ORDER BY LevelOfUsing, Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbCountry.AddItems(bind);
        }
        private void FillComboRegion()
        {
            string query = "SELECT Id, Name FROM Region ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbRegion.AddItems(bind);
        }
        private void FillComboStudyLevel()
        {
            string query = "SELECT DISTINCT StudyLevelId AS Id, StudyLevelName AS Name FROM [Entry] ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbStudyLevel.AddItems(bind);
        }

        private void FillComboStudyForm()
        {
            string query = "SELECT DISTINCT StudyFormId AS Id, StudyFormName AS Name FROM [Entry] ORDER BY 1";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbStudyForm.AddItems(bind);
        }
        private void FillComboStudyBasis()
        {
            string query = "SELECT DISTINCT StudyBasisId AS Id, StudyBasisName AS Name FROM [Entry] ORDER BY 1";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbStudyBasis.AddItems(bind);
        }

        private void FillComboLicenseProgram()
        {
            string query = "SELECT DISTINCT LicenseProgramId AS Id, LicenseProgramName AS Name FROM Entry WHERE CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@CampaignYear", Util.CampaignYear);
            if (StudyLevelId.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevelId);

            }
            if (SemesterId.HasValue)
            {
                query += " AND SemesterId=@SemesterId";
                dic.Add("@SemesterId", SemesterId);

            }
            if (StudyFormId.HasValue)
            {
                query += " AND StudyFormId=@StudyFormId";
                dic.Add("@StudyFormId", StudyFormId);

            }
            if (StudyBasisId.HasValue)
            {
                query += " AND StudyBasisId=@StudyBasisId";
                dic.Add("@StudyBasisId", StudyBasisId);
            }
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbLicenseProgram.AddItems(bind);
        }
        private void FillComboObrazProgram()
        {
            string query = "SELECT Distinct ObrazProgramId AS Id, ObrazProgramName AS Name FROM Entry WHERE CampaignYear=@CampaignYear";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@CampaignYear", Util.CampaignYear);
            if (StudyLevelId.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevelId);

            }
            if (SemesterId.HasValue)
            {
                query += " AND SemesterId=@SemesterId";
                dic.Add("@SemesterId", SemesterId);

            }
            if (StudyFormId.HasValue)
            {
                query += " AND StudyFormId=@StudyFormId";
                dic.Add("@StudyFormId", StudyFormId);

            }
            if (StudyBasisId.HasValue)
            {
                query += " AND StudyBasisId=@StudyBasisId";
                dic.Add("@StudyBasisId", StudyBasisId);
            }
            if (LicenseProgramId.HasValue)
            {
                query += " AND LicenseProgramId=@LicenseProgramId";
                dic.Add("@LicenseProgramId", LicenseProgramId);
            } 
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbObrazProgram.AddItems(bind);
        }

        private void FillComboSemester()
        {
            string query = "SELECT Distinct SemesterId as Id, Semester.Name as Name FROM Entry Join Semester on SemesterId = Semester.Id WHERE  Entry.CampaignYear=@CampaignYear ";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@CampaignYear", Util.CampaignYear);
            if (StudyLevelId.HasValue)
            {
                query += " AND StudyLevelId=@StudyLevelId";
                dic.Add("@StudyLevelId", StudyLevelId);
            }  
            query += " order by Id";
            DataTable tbl = Util.BDC.GetDataTable(query, dic);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbSemester.AddItems(bind);
        }

        private void FillCard()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                Person p = context.Person.Where(x => x.Id == _PersonId).FirstOrDefault();

                Surname = p.Surname;
                personName = p.Name;
                SecondName = p.SecondName;
                FIO = Surname + " " + personName + " " + SecondName;
                BirthDate = p.BirthDate ?? DateTime.Now;
                BirthPlace = p.BirthPlace;
                Sex = p.Sex ?? false;
                NationalityId = p.NationalityId ?? Util.CountryRussiaId;

                PassportSeries = p.PassportSeries;
                PassportNumber = p.PassportNumber;
                PassportAuthor = p.PassportAuthor;
                PassportTypeId = p.PassportTypeId ?? 1;
                PassportDate = p.PassportDate ?? DateTime.Now;
                PassportCode = p.PassportCode;
                SNILS = p.SNILS;
                //------------------------------------------------------

                var PersonCurrentEducation = p.PersonCurrentEducation;
                if (PersonCurrentEducation == null)
                    PersonCurrentEducation = new PersonCurrentEducation();
                
                
                
                StudyLevelId = PersonCurrentEducation.StudyLevelId.Value;
                FillComboSemester();
                SemesterId = PersonCurrentEducation.SemesterId.Value;
                FillComboStudyForm();
                if (PersonCurrentEducation.StudyFormId.HasValue)
                    StudyFormId = PersonCurrentEducation.StudyFormId.Value;
                FillComboStudyBasis();
                if (PersonCurrentEducation.StudyBasisId.HasValue)
                    StudyBasisId = PersonCurrentEducation.StudyBasisId.Value;
                FillComboLicenseProgram();
                if (PersonCurrentEducation.LicenseProgramId.HasValue)
                    LicenseProgramId = PersonCurrentEducation.LicenseProgramId.Value;
                FillComboObrazProgram();
                if (PersonCurrentEducation.ObrazProgramId.HasValue)
                    ObrazProgramId = PersonCurrentEducation.ObrazProgramId.Value;

                ProfileName = PersonCurrentEducation.ProfileName;
              
                var PersonContacts = p.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PriemForeignInspector.PersonContacts();

                Phone = PersonContacts.Phone;
                Mobiles = PersonContacts.Mobiles;
                Email = p.User.Email;

                CountryId = PersonContacts.CountryId ?? Util.CountryRussiaId;
                RegionId = PersonContacts.RegionId ?? 1;

                Code = PersonContacts.Code;
                City = PersonContacts.City;
                Street = PersonContacts.Street;
                House = PersonContacts.House;
                Korpus = PersonContacts.Korpus;
                Flat = PersonContacts.Flat;
                CodeKLADR = PersonContacts.KladrCode;

                CodeReal = PersonContacts.CodeReal;
                CityReal = PersonContacts.CityReal;
                StreetReal = PersonContacts.StreetReal;
                HouseReal = PersonContacts.HouseReal;
                KorpusReal = PersonContacts.KorpusReal;
                FlatReal = PersonContacts.FlatReal;
                //------------------------------------------------------

                var PersonAddInfo = p.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PriemForeignInspector.PersonAddInfo();

                Parents = PersonAddInfo.Parents;
                AddInfo = PersonAddInfo.AddInfo;

                var PersonChangeStudyFormReason = p.PersonChangeStudyFormReason;
                if (PersonChangeStudyFormReason == null)
                    PersonChangeStudyFormReason = new PersonChangeStudyFormReason();
                ChangeStudyBasisReason = PersonChangeStudyFormReason.Reason;

                IsDisabled = p.IsDisabled ?? false;
                HostelEduc = PersonAddInfo.HostelEduc;
                bFillcombos = true;
            }
            CheckBtnDisable();

            string query = @"SELECT [Application].Id, LicenseProgramName AS 'Направление', ObrazProgramName AS 'Образовательная программа', 
            ProfileName AS 'Профиль', SemesterId as 'Семестр',  IsCommited, IsDeleted, Enabled,IsApprovedByComission,
            case when ((select top 1 Application.SecondTypeId from Application where PersonId = @Id and IsCommited = 1)=2) 
				then
					(case 
						when ((Select CountryEducId from PersonEducationDocument where PersonId=@Id )=193)
						then (select [AbiturientType].[Description] from AbiturientType where Id = 3) 
						else (select [AbiturientType].[Description] from AbiturientType where Id = 4) 
						end ) 
					else 
					(Select [AbiturientType].[Description] from AbiturientType where AppSecondTypeId = Application.SecondTypeId) end  AS 'Тип' 
            FROM [Application] 
            INNER JOIN Entry ON Entry.Id = [Application].EntryId 
            WHERE PersonId=@Id  and IsCommited = 1
            --AND Enabled='True'  
            order by 'Тип', LicenseProgramName";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", _PersonId } });
            dgvApps.DataSource = tbl;
            dgvApps.Columns["Id"].Visible = false;
            dgvApps.Columns["Enabled"].Visible = false;
            dgvApps.Columns["IsCommited"].Visible = false;
            dgvApps.Columns["IsDeleted"].Visible = false;
            dgvApps.Columns["IsApprovedByComission"].Visible = false;

        }

        private void CheckBtnDisable()
        {
            if (!IsDisabled)
                btnDisable.Text = "Скрыть из списка";
            else
                btnDisable.Text = "Вернуть в список";
        }

        private void dgvApps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Guid id = (Guid)dgvApps.Rows[e.RowIndex].Cells["Id"].Value;
            Util.OpenAbiturientCard(this.Owner, id, _AbitType);
        }
        private void dgvApps_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           /* bool Enabled = (bool)dgvApps.Rows[e.RowIndex].Cells["Enabled"].Value;
            if (!Enabled)
                e.CellStyle.BackColor = Color.Red;
            bool IsCommited = (bool)dgvApps.Rows[e.RowIndex].Cells["IsCommited"].Value;
            bool IsDeleted = (bool)dgvApps.Rows[e.RowIndex].Cells["IsDeleted"].Value;
            if ((!IsCommited) || (IsDeleted))
            {
                e.CellStyle.BackColor = Color.Tomato;
                e.CellStyle.ForeColor = Color.Black;
            } */
            bool IsApprovedByComission = (bool)dgvApps.Rows[e.RowIndex].Cells["IsApprovedByComission"].Value;
            if (IsApprovedByComission)
                e.CellStyle.BackColor = Color.LightGreen;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            ChangeDisabled();
            _handler();
        }

        public void ChangeDisabled()
        {
            string query = "UPDATE Person SET IsDisabled=@IsDisabled WHERE Id=@Id";
            Dictionary<string, object> dic = new Dictionary<string,object>();
            dic.Add("@IsDisabled", !IsDisabled);
            dic.Add("@Id", _PersonId);
            Util.BDC.ExecuteQuery(query, dic);
            IsDisabled = !IsDisabled;
            CheckBtnDisable();
        }

        private void Save()
        {
            if (!_isOpen)
                Lock();
            else
            {
                Lock();
                SaveCard();
                if (_handler != null)
                    _handler();
            }
        }
        private void SaveCard()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var Person = context.Person.Where(x => x.Id == _PersonId).FirstOrDefault();
                if (Person == null)
                {
                    MessageBox.Show("Ошибка при получении данных Person");
                    return;
                }

                var PersonContacts = Person.PersonContacts;
                if (PersonContacts == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonContacts");
                    return;
                }

                var PersonCurrentEducation = Person.PersonCurrentEducation;
                if (PersonCurrentEducation == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonCurrentEducation");
                    return;
                }

                var PersonAddInfo = Person.PersonAddInfo;
                if (PersonAddInfo == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonAddInfo");
                    return;
                }

                var PersonChangeStudyFormReason = Person.PersonChangeStudyFormReason;
                if (PersonChangeStudyFormReason == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonChangeStudyFormReason");
                    return;
                }

                Person.Surname = Surname;
                Person.Name = personName;
                Person.BirthDate = BirthDate;
                Person.BirthPlace = BirthPlace;
                Person.Sex = Sex;
                Person.PassportSeries = PassportSeries;
                Person.PassportNumber = PassportNumber;
                Person.PassportAuthor = PassportAuthor;
                Person.PassportDate = PassportDate;
                Person.PassportCode = PassportCode;
                Person.SNILS = SNILS;
                Person.NationalityId = NationalityId;

                PersonContacts.Code = Code;
                PersonContacts.City = City;
                PersonContacts.Street = Street;
                PersonContacts.House = House;
                PersonContacts.Korpus = Korpus;
                PersonContacts.Flat = Flat;
                PersonContacts.KladrCode = CodeKLADR;

                PersonContacts.CodeReal = CodeReal;
                PersonContacts.CityReal = CityReal;
                PersonContacts.StreetReal = StreetReal;
                PersonContacts.HouseReal = HouseReal;
                PersonContacts.KorpusReal = KorpusReal;
                PersonContacts.FlatReal = FlatReal; 

                PersonContacts.Phone = Phone;
                PersonContacts.Mobiles = Mobiles;
                PersonContacts.CountryId = CountryId;
                PersonContacts.RegionId = RegionId;

                PersonCurrentEducation.SemesterId = SemesterId;
                PersonCurrentEducation.StudyLevelId = StudyLevelId;
                PersonCurrentEducation.LicenseProgramId = LicenseProgramId;
                PersonCurrentEducation.ObrazProgramId = ObrazProgramId;
                PersonCurrentEducation.ProfileName = ProfileName;

                PersonAddInfo.AddInfo = AddInfo;
                PersonAddInfo.Parents = Parents;

                PersonAddInfo.HostelEduc = HostelEduc;
                PersonChangeStudyFormReason.Reason = ChangeStudyBasisReason;
                context.SaveChanges();
            }
        }
        private void Lock()
        {
            _isOpen = !_isOpen;

            Util.SetAllControlsEnabled(this, _isOpen);

            btnDisable.Enabled = true;
            btnHistory.Enabled = true;
            btnSave.Enabled = true;

            btnSave.Text = _isOpen ? "Сохранить" : "Изменить";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void PersonCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isOpen)
            {
                DialogResult dr = MessageBox.Show("Сохранить изменения в открытой карточке?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                
                if (dr == System.Windows.Forms.DialogResult.Yes)
                    Save();
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            new HistoryCard(_PersonId).ShowDialog();
        }

        private void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFillcombos)
                FillComboSemester();
        }

        private void chbHostelEducYes_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducNo.Checked = !chbHostelEducYes.Checked;
            /*if (_Id != null)
                btnGetAssignToHostel.Enabled = chbHostelEducYes.Checked;*/
        }

        private void chbHostelEducNo_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducYes.Checked = !chbHostelEducNo.Checked;
        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFillcombos)
            FillComboObrazProgram();
        }

        private void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFillcombos)
                FillComboLicenseProgram();
        }

        private void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFillcombos)
                FillComboLicenseProgram();
        }

        private void cbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bFillcombos)
            FillComboLicenseProgram();
        }
    }
}
