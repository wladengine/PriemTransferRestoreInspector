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
    public partial class CardPerson : Form
    {
        private Guid _PersonId;
        private bool _isOpen;
        public UpdateHandler _handler;
        private int _AbitType;
        int _currentEducRow;
        List<PersonEducationDocument> PersonEducDocument;

        public CardPerson(Guid id, int AbitType)
        {
            this.CenterToParent();
            InitializeComponent();
            _PersonId = id;
            _AbitType = AbitType;
            FillText();
            FillCombos();
            FillCard();
            this.Icon = PriemForeignInspector.Properties.Resources.Person_Male_Light;
            _isOpen = false;
        }

        private void FillText()
        {
            switch (_AbitType)
            {
                case 1:
                    {
                        this.Text = "";
                        break;
                    }
                case 2:
                    {
                        this.Text = "Перевод в СПБГУ из Российского ВУЗа";
                        break;
                    }
                case 3:
                    {
                        this.Text = "";
                        break;
                    }
                case 4:
                    {
                        this.Text = "";
                        break;
                    }
                default:
                        {
                            break;
                        }
            }
        }
        private void InitSetHandlers()
        {
            cbCurrentEducationSemester.SelectedIndexChanged += new EventHandler(cbCurrentEducationSemester_SelectedIndexChanged);
            cbCurrentEducationStudyLevel.SelectedIndexChanged += new EventHandler(cbCurrentEducationStudyLevel_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
        }
        private void InitDeleteHandlers()
        {
            cbCurrentEducationSemester.SelectedIndexChanged -= new EventHandler(cbCurrentEducationSemester_SelectedIndexChanged);
            cbCurrentEducationStudyLevel.SelectedIndexChanged-= new EventHandler(cbCurrentEducationStudyLevel_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged -= new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged -= new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged -= new EventHandler(cbStudyForm_SelectedIndexChanged);
        }
        private void FillCombos()
        {
            FillComboPassportType();
            FillComboNationality();
            FillComboCountry();
            FillComboRegion();
            FillComboSchoolType();
            FillComboCountryEduc();
            InitDeleteHandlers();
            FillComboCurrentEducationStudyLevel();
            FillComboCurrentEducationSemester();
            FillComboCurrentStudyForm();
            FillComboCurrentStudyBasis();
            InitSetHandlers();
            //FillComboCurrentLicenseProgram();
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
            cbRegionEduc.AddItems(bind);
        }
        private void FillComboSchoolType()
        {
            string query = "SELECT Id, Name FROM SchoolTypeAll ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbSchoolType.AddItems(bind);
        }
        private void FillComboCountryEduc()
        {
            string query = "SELECT Id, ISNULL(Name, NameEng) AS Name FROM Country ORDER BY LevelOfUsing, Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbCountryEduc.AddItems(bind);
        }
        private void FillComboCurrentEducationStudyLevel()
        {
            string query = "SELECT Distinct StudyLevelId AS Id, StudyLevelName AS Name FROM Entry ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<string, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbCurrentEducationStudyLevel, bind, true, false);
        }
        private void FillComboCurrentEducationSemester()
        {
            string query = "SELECT Id, Name FROM Semester ORDER BY 1";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<string, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbCurrentEducationSemester, bind, true, false);
        }
        private void FillComboCurrentStudyForm()
        {
            string query = "SELECT Id, Name FROM StudyForm ORDER BY 1";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbStudyForm.AddItems(bind);
        }
        private void FillComboCurrentStudyBasis()
        {
            string query = "SELECT Id, Name FROM StudyBasis ORDER BY 1";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbStudyBasis.AddItems(bind);
        }
        private void FillComboCurrentLicenseProgram()
        {
            if (!CurrentEducationStudyLevelId.HasValue)
            {
                List<KeyValuePair<object, string>> bind = new List<KeyValuePair<object, string>>();
                cbLicenseProgram.AddItems(bind);
                return;
            }
            else
            {
                string query = @"SELECT DISTINCT LicenseProgramId, LicenseProgramCode, LicenseProgramName 
                             FROM Entry WHERE StudyLevelId=@StudyLevelId ORDER BY LicenseProgramCode, LicenseProgramName";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@StudyLevelId", CurrentEducationStudyLevelId);

                DataTable tbl = Util.BDC.GetDataTable(query, dic);
                List<KeyValuePair<object, string>> bind =
                    (from DataRow rw in tbl.Rows
                     orderby rw.Field<string>("LicenseProgramCode")
                     select new KeyValuePair<object, string>(
                         rw.Field<int>("LicenseProgramId"),
                         "(" + rw.Field<string>("LicenseProgramCode") + ") " + rw.Field<string>("LicenseProgramName")
                     )).ToList();
                cbLicenseProgram.AddItems(bind);
            }
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
                Sex = p.Sex;
                NationalityId = p.NationalityId ?? Util.CountryRussiaId;

                PassportSeries = p.PassportSeries;
                PassportNumber = p.PassportNumber;
                PassportAuthor = p.PassportAuthor;
                PassportTypeId = p.PassportTypeId ?? 1;
                PassportDate = p.PassportDate ?? DateTime.Now;
                PassportCode = p.PassportCode;
                SNILS = p.SNILS;
                //------------------------------------------------------

                var PersonContacts = p.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PriemForeignInspector.PersonContacts();

                Phone = PersonContacts.Phone;
                Email = p.User.Email;
                Mobiles = PersonContacts.Mobiles;

                CountryId = PersonContacts.CountryId;
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

                var PersonCurrentEducation = p.PersonCurrentEducation;
                if (PersonCurrentEducation == null)
                    PersonCurrentEducation = new PriemForeignInspector.PersonCurrentEducation();

                HasAccreditation = PersonCurrentEducation.HasAccreditation;
                AccreditationDate = PersonCurrentEducation.AccreditationDate;
                AccreditationNumber = PersonCurrentEducation.AccreditationNumber;
                HasScholarship = PersonCurrentEducation.HasScholarship;
                if (PersonCurrentEducation.SemesterId > 0)
                    CurrentEducationSemesterId = PersonCurrentEducation.SemesterId;
                if (PersonCurrentEducation.StudyLevelId > 0)
                    CurrentEducationStudyLevelId = PersonCurrentEducation.StudyLevelId;
                FillComboCurrentLicenseProgram();
                CurrentLicenseProgramId = PersonCurrentEducation.LicenseProgramId;
                StudyBasisId = PersonCurrentEducation.StudyBasisId ?? 1;
                StudyFormId = PersonCurrentEducation.StudyFormId ?? 1;
                //------------------------------------------------------
                var PersonAddInfo = p.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PriemForeignInspector.PersonAddInfo();
                Parents = PersonAddInfo.Parents;
                AddInfo = PersonAddInfo.AddInfo;
                IsDisabled = p.IsDisabled ?? false;
                HostelEduc = PersonAddInfo.HostelEduc;
            }
            CheckBtnDisable();

            FillEducationData(_PersonId);

            var CountryEducId = (int) Util.BDC.GetValue(@"Select top 1 CountryEducId from PersonEducationDocument where 
								PersonId=@Id and SchoolTypeId = 4  order by Id", new Dictionary<string, object>() { { "@Id", _PersonId } });
            int CountryEduc = -1;
            if (CountryEducId != null)
                if (!int.TryParse(CountryEducId.ToString(), out CountryEduc))
                {
                    CountryEduc = -1;
                }

            string query = @"SELECT [Application].Id,[Application].CommitId, LicenseProgramName AS 'Направление', ObrazProgramName AS 'Образовательная программа', 
            ProfileName AS 'Профиль', SemesterId as 'Семестр',  IsCommited, IsDeleted, Enabled, 
            IsApprovedByComission,
            case when (Application.SecondTypeId =2) 
				then (
					" + (CountryEduc >0 ? (CountryEduc == 193 ? 
                      @"select [AbiturientType].[Description] from AbiturientType where Id = 3" :
                      @"select [AbiturientType].[Description] from AbiturientType where Id = 4" ):"перевод в СПбГУ" )+ 
				@") else  (Select [AbiturientType].[Description] from AbiturientType where AppSecondTypeId = Application.SecondTypeId) end  AS 'Тип' 
            FROM [Application] 
            INNER JOIN Entry ON Entry.Id = [Application].EntryId 
            WHERE PersonId=@Id  and IsCommited = 1 
            order by 'Тип', LicenseProgramName";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", _PersonId } });
            dgvApps.DataSource = tbl;
            dgvApps.Columns["Id"].Visible = false;
            dgvApps.Columns["CommitId"].Visible = false;
            dgvApps.Columns["Enabled"].Visible = false;
            dgvApps.Columns["IsCommited"].Visible = false;
            dgvApps.Columns["IsDeleted"].Visible = false;
            dgvApps.Columns["IsApprovedByComission"].Visible = false;
        }
        private void FillEducationData(Guid _PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
            var lstVals = (from x in context.Person
                           join ed in context.PersonEducationDocument on x.Id equals ed.PersonId
                           where x.Id == _PersonId
                           select ed).ToList();

            PersonEducDocument = lstVals;

            dgvEducation.DataSource = lstVals.Select(x => new
            {
                x.Id,
                School = x.SchoolName,
                Series = x.Series,
                Num = x.Number,
            }).ToList();

            dgvEducation.Columns["Id"].Visible = false;
            dgvEducation.Columns["School"].HeaderText = "Уч. учреждение";
            dgvEducation.Columns["School"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvEducation.Columns["Series"].HeaderText = "Серия";
            dgvEducation.Columns["Series"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvEducation.Columns["Num"].HeaderText = "Номер";
            dgvEducation.Columns["Num"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            if (lstVals.Count > 0)
                ViewEducationInfo(lstVals.First().Id);

            _currentEducRow = 0;
            }
        }
        private void ViewEducationInfo(int EducId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                var lstVals = PersonEducDocument.Where(x => x.Id == EducId).Select(x => x).First();
                var vEntryYear = (from x in context.PersonHighEducationInfo
                                 where x.EducationDocumentId == EducId
                                 select x.EntryYear).FirstOrDefault();

                CountryEducId = lstVals.CountryEducId;
                SchoolTypeId = lstVals.SchoolTypeId;
                EducationName = lstVals.SchoolName;
                EducationDocumentSeries = lstVals.Series;
                EducationDocumentNumber = lstVals.Number;
                RegionEduc = lstVals.RegionEducId;

                int ex_year;
                if (int.TryParse(lstVals.SchoolExitYear, out ex_year))
                    ExitYear = ex_year;
                else
                    ExitYear = null;
                if (vEntryYear.HasValue)
                    EntryYear = vEntryYear.Value;
                else
                    EntryYear = null;

            }
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
                #region Person
                var Person = context.Person.Where(x => x.Id == _PersonId).FirstOrDefault();
                if (Person == null)
                {
                    MessageBox.Show("Ошибка при получении данных Person");
                    return;
                }
                else
                {
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
                }
                #endregion
                #region PersonContacts
                var PersonContacts = Person.PersonContacts;
                if (PersonContacts == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonContacts");
                    return;
                }
                else
                {
                    PersonContacts.Phone = Phone;
                    PersonContacts.Mobiles = Mobiles;

                    PersonContacts.CountryId = CountryId ?? Util.CountryRussiaId;
                    PersonContacts.RegionId = RegionId;
                    PersonContacts.KladrCode = CodeKLADR;

                    PersonContacts.Code = Code;
                    PersonContacts.City = City;
                    PersonContacts.Street = Street;
                    PersonContacts.House = House;
                    PersonContacts.Korpus = Korpus;
                    PersonContacts.Flat = Flat;

                    PersonContacts.CodeReal = CodeReal;
                    PersonContacts.CityReal = CityReal;
                    PersonContacts.StreetReal = StreetReal;
                    PersonContacts.HouseReal = HouseReal;
                    PersonContacts.KorpusReal = KorpusReal;
                    PersonContacts.FlatReal = FlatReal;
                }
                #endregion
                #region PersonAddInfo
                var PersonAddInfo = Person.PersonAddInfo;
                if (PersonAddInfo == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonAddInfo");
                    return;
                }
                else
                {
                    PersonAddInfo.Parents = Parents;
                    PersonAddInfo.AddInfo = AddInfo;
                    PersonAddInfo.HostelEduc = HostelEduc;
                }
                #endregion

                var PersonCurrentEducation = Person.PersonCurrentEducation;
                // смотря какая карточка (+ создать новое)
                if (PersonCurrentEducation == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonCurrentEducation");
                    return;
                }

                PersonCurrentEducation.AccreditationDate = AccreditationDate;
                PersonCurrentEducation.AccreditationNumber = AccreditationNumber;
                PersonCurrentEducation.EducName = "";
                PersonCurrentEducation.HasAccreditation = HasAccreditation;
                PersonCurrentEducation.HasScholarship = HasScholarship;
                PersonCurrentEducation.SemesterId = CurrentEducationSemesterId ?? 3;
                PersonCurrentEducation.StudyLevelId = CurrentEducationStudyLevelId ?? 16;
                PersonCurrentEducation.LicenseProgramId = CurrentLicenseProgramId ?? 1;

                int ind = PersonEducDocument.FindIndex(x => x.Id == CurrEducationId);
                if (ind >= 0)
                {
                    PersonEducDocument[ind].SchoolTypeId = SchoolTypeId;
                    PersonEducDocument[ind].SchoolName = EducationName;
                    PersonEducDocument[ind].SchoolExitYear = ExitYear.ToString();
                    PersonEducDocument[ind].CountryEducId = CountryEducId;
                    PersonEducDocument[ind].RegionEducId = RegionEduc.Value;
                    PersonEducDocument[ind].Series = EducationDocumentSeries;
                    PersonEducDocument[ind].Number = EducationDocumentNumber;
                    PersonEducDocument[ind].IsEqual = IsEqual;
                    PersonEducDocument[ind].EqualDocumentNumber = EqualDocumentNumber;

                    //EntryYear

                    dgvEducation["School", ind].Value = EducationName;
                    dgvEducation["Series", ind].Value = EducationDocumentSeries;
                    dgvEducation["Num", ind].Value = EducationDocumentNumber;
                }
                


                context.SaveChanges();
            }
        }
        private void Lock()
        {
            _isOpen = !_isOpen;

            tbSurname.Enabled = _isOpen;
            tbName.Enabled = _isOpen;
            tbPassportSeries.Enabled = _isOpen;
            tbPassportNumber.Enabled = _isOpen;
            tbBirthPlace.Enabled = _isOpen;
            dtpBirthDate.Enabled = _isOpen;
            dtpPassportDate.Enabled = _isOpen;

            tbEducationName.Enabled = _isOpen;
            tbExitYear.Enabled = _isOpen;


            tbPhone.Enabled = _isOpen;

            cbNationality.Enabled = _isOpen;

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

        private void chbHostelEducYes_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducNo.Checked = !chbHostelEducYes.Checked; 
        }

        private void chbHostelEducNo_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducYes.Checked = !chbHostelEducNo.Checked;
        }

        private void dgvEducation_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvEducation.CurrentCell == null)
                return;

            if (_currentEducRow != dgvEducation.CurrentRow.Index)
            {
                _currentEducRow = dgvEducation.CurrentRow.Index;
                CurrEducationId = int.Parse(dgvEducation.CurrentRow.Cells["Id"].Value.ToString());
                ViewEducationInfo(CurrEducationId);
            }

        }

        private void cbCurrentEducationStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbCurrentEducationSemester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEducationSave_Click(object sender, EventArgs e)
        {

        }

    }
}
