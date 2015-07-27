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
        public int _AbitType;
        int _currentEducRow;
        List<PersonEducationDocument> PersonEducDocument;
        public bool HasCurrentEdication;
        public bool HasDisorderInfo;

        public CardPerson()
        { }
        public CardPerson(Guid id)
        {
            this.CenterToParent();
            this.MdiParent = Util.MainForm;

            InitializeComponent();
            _PersonId = id; 
            FillCombos();
            FillCard(); 
            this.Icon = PriemForeignInspector.Properties.Resources.Person_Male_Light;
            _isOpen = false;
        }

        protected void FillText(int _AbitType)
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
                        this.Text = "Восстановление в СПбГУ";
                        break;
                    }
                case 4:
                    {
                        this.Text = "Перевода в СПБГУ из иностранного ВУЗа";
                        break;
                    }
                case 5:
                    {
                        this.Text = "Перевод с платной основы обучения на бюджетную (смена основы обучения)";
                        break;
                    }
                case 6:
                    {
                        this.Text = "Смена образовательной программы";
                        break;
                    }
                default:
                        {
                            break;
                        }
            }
        }
        protected void InitSetHandlers()
        {
            cbCurrentEducationSemester.SelectedIndexChanged += new EventHandler(cbCurrentEducationSemester_SelectedIndexChanged);
            cbCurrentEducationStudyLevel.SelectedIndexChanged += new EventHandler(cbCurrentEducationStudyLevel_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged += new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged += new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged += new EventHandler(cbStudyForm_SelectedIndexChanged);
            dgvEducation.CurrentCellChanged += new EventHandler(dgvEducation_CurrentCellChanged);

        }
        protected void InitDeleteHandlers()
        {
            cbCurrentEducationSemester.SelectedIndexChanged -= new EventHandler(cbCurrentEducationSemester_SelectedIndexChanged);
            cbCurrentEducationStudyLevel.SelectedIndexChanged-= new EventHandler(cbCurrentEducationStudyLevel_SelectedIndexChanged);
            cbLicenseProgram.SelectedIndexChanged -= new EventHandler(cbLicenseProgram_SelectedIndexChanged);
            cbStudyBasis.SelectedIndexChanged -= new EventHandler(cbStudyBasis_SelectedIndexChanged);
            cbStudyForm.SelectedIndexChanged -= new EventHandler(cbStudyForm_SelectedIndexChanged);
            dgvEducation.CurrentCellChanged -= new EventHandler(dgvEducation_CurrentCellChanged);
        }
        
        #region ChangeVisibility
        protected void ChangeVisiblegbCurrentEducation(bool Visible)
        {
            gbCurrenctEducationInfo.Visible = Visible;
        }
        protected void ChangeVisiblegbDisorderInfo(bool Visible)
        {
            gbDisorderInfo.Location = gbCurrenctEducationInfo.Location;
            gbDisorderInfo.Visible = Visible;
        }
        protected void ChangeVisibleCurrentObrazProgramProfile(bool Visible)
        {
            tbCurrentProfile.Visible =
                cbCurrentObrazProgram.Visible =
                lbCurrentProfile.Visible =
                lblCurrentObrazProgram.Visible =
               Visible;
        }
        protected void ChangeVisiblegbReason(bool Visible)
        {
           gbReason.Visible = 
               Visible;
        }
        protected void ChangeVisibleIsEqual(bool Visible)
        {
            gbEqualInfo.Visible = chbIsEqual.Visible = 
                Visible;
        }
        protected void ChangeVisibleAccreditationInfo(bool Visible)
        {
            gbAccreditationInfo.Visible = chbHasAccreditation.Visible =
                Visible;
        }

        #endregion
        #region FillCombos
        protected void FillCombos()
        {
            FillComboPassportType();
            FillComboNationality();
            FillComboCountry(); 
            FillComboSchoolType();
            FillComboCountryEduc();
            InitDeleteHandlers();
            FillComboCurrentEducationStudyLevel();
            FillComboCurrentEducationSemester();
            FillComboCurrentStudyForm();
            FillComboCurrentStudyBasis();
            FillComboCurrentLicenseProgram();
            InitSetHandlers();
        }
        protected void FillComboPassportType()
        {
            string query = "SELECT Id, Name FROM PassportType ORDER BY Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbPassportType.AddItems(bind);
        }
        protected void FillComboNationality()
        {
            string query = "SELECT Id, ISNULL(Name, NameEng) AS Name FROM Country ORDER BY LevelOfUsing, Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbNationality.AddItems(bind);
        }
        protected void FillComboCountry()
        {
            string query = "SELECT Id, ISNULL(Name, NameEng) AS Name FROM Country ORDER BY LevelOfUsing desc, Name";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbCountry.AddItems(bind);
        }
        protected void FillComboRegion()
        {
            string query = @"SELECT Region.Id, Region.Name FROM dbo.Region 
" + (CountryId == 193 ? " where RegionNumber >0 " : " left join dbo.Country on Country.RegionId = Region.Id where Country.Id = @CountryId ") +
" ORDER BY 2 ";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string,object>(){{"@CountryId", CountryId}});
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbRegion.AddItems(bind);
        }
        protected void FillComboRegionEduc()
        {
            string query = @"SELECT Region.Id, Region.Name FROM dbo.Region 
" + (CountryEducId == 193 ? " where RegionNumber >0 " : " left join dbo.Country on Country.RegionId = Region.Id where Country.Id = @CountryId ") +
" ORDER BY 2 "; 
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@CountryId", CountryEducId } });
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
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
            if (!CurrentEducationStudyLevelId.HasValue || !CurrentEducationSemesterId.HasValue || !StudyFormId.HasValue || !StudyBasisId.HasValue)
            {
                List<KeyValuePair<object, string>> bind = new List<KeyValuePair<object, string>>();
                cbLicenseProgram.AddItems(bind);
                return;
            }
            else
            {
                string query = @"SELECT DISTINCT LicenseProgramId, LicenseProgramCode, LicenseProgramName 
                             FROM Entry 
                             WHERE StudyLevelId=@StudyLevelId and SemesterId = @SemesterId and StudyBasisId=@StudyBasisId and StudyFormId=@StudyFormId
                             ORDER BY LicenseProgramCode, LicenseProgramName";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@StudyLevelId", CurrentEducationStudyLevelId);
                dic.Add("@SemesterId", CurrentEducationSemesterId);
                dic.Add("@StudyBasisId", StudyBasisId);
                dic.Add("@StudyFormId", StudyFormId);

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
        private void FillComboCurrentObrazProgram()
        {
            if (!CurrentEducationStudyLevelId.HasValue || !CurrentEducationSemesterId.HasValue || !StudyFormId.HasValue || !StudyBasisId.HasValue || !CurrentLicenseProgramId.HasValue)
            {
                List<KeyValuePair<object, string>> bind = new List<KeyValuePair<object, string>>();
                cbLicenseProgram.AddItems(bind);
                return;
            }
            else
            {
                string query = @"SELECT DISTINCT ObrazProgramId, ObrazProgramName 
                             FROM Entry 
                             WHERE StudyLevelId=@StudyLevelId and SemesterId = @SemesterId and StudyBasisId=@StudyBasisId and StudyFormId=@StudyFormId
                             and LicenseProgramId = @LicenseProgramId
                             ORDER BY ObrazProgramName";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@StudyLevelId", CurrentEducationStudyLevelId);
                dic.Add("@SemesterId", CurrentEducationSemesterId);
                dic.Add("@StudyBasisId", StudyBasisId);
                dic.Add("@StudyFormId", StudyFormId);
                dic.Add("@LicenseProgramId", CurrentLicenseProgramId);

                DataTable tbl = Util.BDC.GetDataTable(query, dic);
                List<KeyValuePair<object, string>> bind =
                    (from DataRow rw in tbl.Rows
                     orderby rw.Field<string>("ObrazProgramName")
                     select new KeyValuePair<object, string>(
                         rw.Field<int>("ObrazProgramId"),
                         rw.Field<string>("ObrazProgramName")
                     )).ToList();
                cbLicenseProgram.AddItems(bind);
            }
        }
        #endregion
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
                //-----------------------------------------------------PersonAddInfo--
                var PersonAddInfo = p.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PriemForeignInspector.PersonAddInfo();
                Parents = PersonAddInfo.Parents;
                AddInfo = PersonAddInfo.AddInfo;
                IsDisabled = p.IsDisabled ?? false;
                HostelEduc = PersonAddInfo.HostelEduc;

                gbTRKI.Visible = !(NationalityId == 193);
                HasTRKI = PersonAddInfo.HasTRKI;
                TRKINumber = PersonAddInfo.TRKICertificateNumber ?? "";
                //-----------------------------------------------------PersonContacts-

                var PersonContacts = p.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PriemForeignInspector.PersonContacts();

                Phone = PersonContacts.Phone;
                Email = p.User.Email;
                Mobiles = PersonContacts.Mobiles;

                CountryId = PersonContacts.CountryId;

                int iRegion = ComboServ.GetComboIdInt(cbRegion) ?? 1;
                RegionId = PersonContacts.RegionId ?? 1;
                if (!RegionId.HasValue)
                    RegionId = iRegion;

                lblRegion.Visible = cbRegion.Visible = (CountryId == Util.CountryRussiaId);

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
                //----------------------------------------------------PERSON CURRENT EDUCATION--
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

                //-----------------------------------------------------PersonDisorderInfo--
                var PersonDisorderInfo = p.PersonDisorderInfo;
                if (PersonDisorderInfo == null)
                    PersonDisorderInfo = new PriemForeignInspector.PersonDisorderInfo();

                DisorderEducationName = PersonDisorderInfo.EducationProgramName;
                YearOfDisorder = PersonDisorderInfo.YearOfDisorder;
                IsForIGA = PersonDisorderInfo.IsForIGA;

                CheckBtnDisable();
                FillEducationData(_PersonId);

                var CountryList = (from x in context.PersonEducationDocument
                                   where x.PersonId == _PersonId
                                   select new
                                   {
                                       x.CountryEducId,
                                       x.SchoolTypeId,
                                   }).ToList();
                int CountryEduc = CountryList.OrderByDescending(x => x.SchoolTypeId).Select(x => x.CountryEducId).First();

                string query = @"SELECT [Application].Id,[Application].CommitId, LicenseProgramName AS 'Направление', ObrazProgramName AS 'Образовательная программа', 
                ProfileName AS 'Профиль', SemesterId as 'Семестр',  IsCommited, IsDeleted, Enabled, 
                IsApprovedByComission,
                case when (Application.SecondTypeId =2) 
				    then (
					    " + (CountryEduc > 0 ? (CountryEduc == 193 ?
                              @"select [AbiturientType].[Description] from AbiturientType where Id = 3" :
                              @"select [AbiturientType].[Description] from AbiturientType where Id = 4") : "'перевод в СПбГУ'") +
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
        }
        private void FillEducationData(Guid _PersonId)
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                PersonEducDocument = context.PersonEducationDocument.Where(x => x.PersonId == _PersonId).ToList();

                var lstVals = (from x in context.Person
                               join ed in context.PersonEducationDocument on x.Id equals ed.PersonId
                               where x.Id == _PersonId
                               select new
                               {
                                   Id = ed.Id,
                                   SchoolName = ed.SchoolName,
                                   Series = ed.Series,
                                   ed.SchoolTypeId,
                                   ed.Number
                               }).ToList();
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

                CurrEducationId = lstVals.First().Id;
                if (lstVals.Count > 0)
                    ViewEducationInfo(CurrEducationId);
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
                SchoolCity = lstVals.SchoolCity;
                    
                EducationName = lstVals.SchoolName;
                EducationDocumentSeries = lstVals.Series;
                EducationDocumentNumber = lstVals.Number;
                    
                lblRegionEduc.Visible = cbRegionEduc.Visible = (CountryEducId == 193);
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
                IsEqual = lstVals.IsEqual;
                EqualDocumentNumber = lstVals.EqualDocumentNumber;
                chbIsEqual.Visible = gbEqualInfo.Visible = (CountryEducId != Util.CountryRussiaId);
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
                    PersonAddInfo.HasTRKI = HasTRKI;
                    PersonAddInfo.TRKICertificateNumber = TRKINumber;
                }
                #endregion

                if (HasCurrentEdication)
                {
                    #region CurrentEducation
                    var CurrentEducation = Person.PersonCurrentEducation;
                    bool bHas = true;
                    if (CurrentEducation == null)
                    {
                        bHas = false;
                        CurrentEducation = new PersonCurrentEducation();
                        CurrentEducation.PersonId = _PersonId;
                    }

                    CurrentEducation.AccreditationDate = AccreditationDate;
                    CurrentEducation.AccreditationNumber = AccreditationNumber;
                    CurrentEducation.EducName = "";
                    CurrentEducation.HasAccreditation = HasAccreditation;
                    CurrentEducation.HasScholarship = HasScholarship;
                    CurrentEducation.SemesterId = CurrentEducationSemesterId ?? 3;
                    CurrentEducation.StudyLevelId = CurrentEducationStudyLevelId ?? 16;
                    CurrentEducation.LicenseProgramId = CurrentLicenseProgramId ?? 1;
                    CurrentEducation.ObrazProgramId = CurrentObrazProgramId;
                    CurrentEducation.ProfileName = ProfileName;
                    if (!bHas)
                        context.PersonCurrentEducation.Add(CurrentEducation);
                    #endregion
                }
                if (HasDisorderInfo)
                {
                    #region PersonDisorderInfo
                    PersonDisorderInfo DisorderInfo = Person.PersonDisorderInfo;
                    bool bHas = true;
                    if (DisorderInfo == null)
                    {
                        bHas = false;
                        DisorderInfo = new PersonDisorderInfo();
                        DisorderInfo.PersonId = _PersonId;
                    }
                    DisorderInfo.EducationProgramName = DisorderEducationName;
                    DisorderInfo.IsForIGA = chbIsForIGA.Checked;
                    DisorderInfo.YearOfDisorder = YearOfDisorder;
                    if (!bHas)
                        context.PersonDisorderInfo.Add(DisorderInfo);
                    #endregion
                }

                context.SaveChanges();
                SaveEducationDocument();

            }
        }
        private void Lock()
        {
            _isOpen = !_isOpen;
            Util.SetAllControlsEnabled(this, _isOpen);
            tbEmail.Enabled = tbEmail.ReadOnly = true;
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
                SaveEducationDocument();
                _currentEducRow = dgvEducation.CurrentRow.Index;
                CurrEducationId = int.Parse(dgvEducation.CurrentRow.Cells["Id"].Value.ToString());
                ViewEducationInfo(CurrEducationId);
            }
        }

        private void cbCurrentEducationStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboCurrentLicenseProgram();
        }

        private void cbCurrentEducationSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboCurrentLicenseProgram();
        }

        private void cbStudyForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboCurrentLicenseProgram();
        }

        private void cbStudyBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboCurrentLicenseProgram();
        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboCurrentObrazProgram();
        }

        private void SaveEducationDocument()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                PersonEducationDocument Doc = context.PersonEducationDocument.Where(x => x.Id == CurrEducationId).Select(x => x).First();
                bool bHas = true;
                if (Doc == null)
                {
                    bHas = false;
                    Doc = new PersonEducationDocument();
                    Doc.PersonId = _PersonId;
                }

                Doc.SchoolTypeId = SchoolTypeId;
                Doc.SchoolCity = SchoolCity;
                Doc.SchoolName = EducationName;
                Doc.SchoolExitYear = ExitYear.ToString();
                Doc.SchoolName = EducationName;
                Doc.RegionEducId = RegionEduc.Value;
                Doc.Series = EducationDocumentSeries;
                Doc.Number = EducationDocumentNumber;
                Doc.IsEqual = chbIsEqual.Checked;
                Doc.EqualDocumentNumber = EqualDocumentNumber;
                if (!bHas)
                    context.PersonEducationDocument.Add(Doc);
                context.SaveChanges();

                if (SchoolTypeId == 4)
                {
                    PersonHighEducationInfo HEduc = context.PersonHighEducationInfo.Where(x => x.EducationDocumentId == CurrEducationId).Select(x => x).FirstOrDefault();
                    bool bHEHas = true;
                    if (HEduc == null)
                    {
                        bHEHas =  false;
                        HEduc = new PersonHighEducationInfo();
                        HEduc.EducationDocumentId = CurrEducationId;
                    }
                    HEduc.EntryYear = EntryYear;
                    if (!bHEHas)
                        context.PersonHighEducationInfo.Add(HEduc);
                }
                context.SaveChanges();
            }
        }

        private void cbCountryEduc_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboRegionEduc();
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboRegion();
        }

    }
}
