using PriemForeignInspector.EDM;
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
        public bool HasCurrentEdication = false; 
        public bool HasDisorderInfo = false;
        public bool HasReason = false;

        public CardPerson()
        { }
        public CardPerson(Guid id)
        {
            this.CenterToParent();
            this.MdiParent = Util.MainForm;

            InitializeComponent();
            _PersonId = id;
            InitFields();
            
            FillCombos();
            FillCard(); 
            this.Icon = PriemForeignInspector.Properties.Resources.Person_Male_Light;
            _isOpen = false;
            FillText();
        }

        protected virtual void FillText()
        {
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
        protected virtual void InitFields()
        {

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
            string query = "SELECT  distinct Semester.Id, Semester.Name FROM Entry join Semester on Entry.SemesterId = Semester.Id ORDER BY 1";
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
                string query = @"
  SELECT LP.Id LicenseProgramId, LP.Code LicenseProgramCode, LP.Name LicenseProgramName
  FROM dbo.SP_LicenseProgram LP
  join dbo.SP_StudyPlanHelp on LP.Id = SP_StudyPlanHelp.LicenseProgramId
  WHERE LP.StudyLevelId=@StudyLevelId and SemesterId = @SemesterId 
  and StudyFormId=@StudyFormId
  ORDER BY LP.Code, LP.Name";
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
                if (bind.Count == 0)
                    cbLicenseProgram.DataSource = new List<KeyValuePair<object, string>>();
                cbLicenseProgram.AddItems(bind);
            }
        }
        private void FillComboCurrentObrazProgram()
        {
            if (!CurrentEducationStudyLevelId.HasValue || !CurrentEducationSemesterId.HasValue || !StudyFormId.HasValue || !StudyBasisId.HasValue || !CurrentLicenseProgramId.HasValue)
            {
                List<KeyValuePair<object, string>> bind = new List<KeyValuePair<object, string>>();
                cbCurrentObrazProgram.AddItems(bind);
                return;
            }
            else
            {
                string query = @"SELECT DISTINCT OP.Id ObrazProgramId, OP.Name ObrazProgramName 
                    FROM SP_ObrazProgram OP
                    join dbo.SP_StudyPlanHelp on OP.Id = SP_StudyPlanHelp.ObrazProgramId 
                    WHERE SP_StudyPlanHelp.LicenseProgramId=@LicenseProgramId
                    and SemesterId = @SemesterId 
                    and StudyFormId=@StudyFormId 
                    ORDER BY ObrazProgramName";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@SemesterId", CurrentEducationSemesterId);
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
                cbCurrentObrazProgram.AddItems(bind);
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
                    PersonAddInfo = new PriemForeignInspector.EDM.PersonAddInfo();
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
                    PersonContacts = new PriemForeignInspector.EDM.PersonContacts();

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
                if (HasCurrentEdication)
                {
                    var PersonCurrentEducation = p.PersonCurrentEducation;
                    if (PersonCurrentEducation == null)
                        PersonCurrentEducation = new PriemForeignInspector.EDM.PersonCurrentEducation();

                    HasAccreditation = PersonCurrentEducation.HasAccreditation;
                    AccreditationDate = PersonCurrentEducation.AccreditationDate;
                    AccreditationNumber = PersonCurrentEducation.AccreditationNumber;
                    HasScholarship = PersonCurrentEducation.HasScholarship;

                    StudyBasisId = PersonCurrentEducation.StudyBasisId ?? 1;
                    StudyFormId = PersonCurrentEducation.StudyFormId ?? 1;

                    if (PersonCurrentEducation.SemesterId > 0)
                        CurrentEducationSemesterId = PersonCurrentEducation.SemesterId;
                    if (PersonCurrentEducation.StudyLevelId > 0)
                        CurrentEducationStudyLevelId = PersonCurrentEducation.StudyLevelId;
                    FillComboCurrentLicenseProgram();
                    CurrentLicenseProgramId = PersonCurrentEducation.LicenseProgramId;
                    FillComboCurrentObrazProgram();
                    CurrentObrazProgramId = PersonCurrentEducation.ObrazProgramId;
                    ProfileName = PersonCurrentEducation.ProfileName ?? "";
                }
                //-----------------------------------------------------PersonDisorderInfo--
                if (HasDisorderInfo)
                {
                    var PersonDisorderInfo = p.PersonDisorderInfo;
                    if (PersonDisorderInfo == null)
                        PersonDisorderInfo = new PriemForeignInspector.EDM.PersonDisorderInfo();

                    DisorderEducationName = PersonDisorderInfo.EducationProgramName;
                    YearOfDisorder = PersonDisorderInfo.YearOfDisorder;
                    IsForIGA = PersonDisorderInfo.IsForIGA;
                }
                //-----------------------------------------------------PersonReason--
                if (HasReason)
                {
                    var PersonReason = p.PersonChangeStudyFormReason;
                    if (PersonReason == null)
                        PersonReason = new PersonChangeStudyFormReason();
                    Reason = PersonReason.Reason ?? "";
                }
                CopyToOldValues();
                //-----------------------------------------------------PersonEducation-
                CheckBtnDisable();
                FillEducationData(_PersonId);
                CopyToOldValues_Education();
                var CountryList = (from x in context.PersonEducationDocument
                                   where x.PersonId == _PersonId
                                   select new
                                   {
                                       x.CountryEducId,
                                       x.SchoolTypeId,
                                   }).ToList();
                int CountryEduc = CountryList.OrderByDescending(x => x.SchoolTypeId).Select(x => x.CountryEducId).First();

                string query = @"SELECT [Application].Id,[Application].CommitId, LicenseProgramName AS 'Направление', ObrazProgramName AS 'Образовательная программа', 
                ProfileName AS 'Профиль', Semester.Name as 'Семестр',  IsCommited, IsDeleted, Enabled, IsViewed,
                IsApprovedByComission,
                
(select top 1 ('№ '+convert(nvarchar,Vers.Id) + ' от '
+convert(nvarchar, Vers.VersionDate, 4) +' '
+convert(nvarchar, Vers.VersionDate, 8)) as Expr 
from dbo.ApplicationCommitVersion Vers 
where Vers.CommitId = [Application].CommitId order by Id desc) as 'Версия',


                case when (Application.SecondTypeId =2) 
				    then (
					    " + (CountryEduc > 0 ? (CountryEduc == 193 ?
                              @"select [AbiturientType].[Description] from AbiturientType where Id = 3" :
                              @"select [AbiturientType].[Description] from AbiturientType where Id = 4") : "'перевод в СПбГУ'") +
                        @") else (Select [AbiturientType].[Description] from AbiturientType where AppSecondTypeId = Application.SecondTypeId) end  AS 'Тип' 
                FROM dbo.[Application] 
                INNER JOIN Entry ON Entry.Id = [Application].EntryId 
                INNER JOIN Semester ON Semester.Id = Entry.SemesterId
                WHERE PersonId=@Id  and IsCommited = 1 and Entry.CampaignYear = @CampaignYear AND Entry.IsUsedForPriem = 1 AND Entry.SemesterId > 1
                order by 'Тип', LicenseProgramName";
                DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", _PersonId }, { "@CampaignYear", Util.CampaignYear } });
                dgvApps.DataSource = tbl;
                dgvApps.Columns["Id"].Visible = false;
                dgvApps.Columns["CommitId"].Visible = false;
                dgvApps.Columns["Enabled"].Visible = false;
                dgvApps.Columns["IsCommited"].Visible = false;
                dgvApps.Columns["IsDeleted"].Visible = false;
                dgvApps.Columns["IsApprovedByComission"].Visible = false;
                dgvApps.Columns["IsViewed"].Visible = false;
                
            }
        }
        private void CopyToOldValues()
        {
            oldSurname = Surname;
            oldpersonName = personName;
            oldSecondName = SecondName;
            oldBirthDate = BirthDate;
            oldBirthPlace = BirthPlace;
            oldSex = Sex;
            oldNationalityId = NationalityId;

            oldPassportSeries = PassportSeries;
            oldPassportNumber = PassportNumber;
            oldPassportAuthor = PassportAuthor;
            oldPassportTypeId = PassportTypeId;
            oldPassportDate = PassportDate;
            oldPassportCode = PassportCode;
            oldSNILS = SNILS;

            oldParents = Parents;
            oldAddInfo = AddInfo;
            oldIsDisabled = IsDisabled;
            oldHostelEduc = HostelEduc;

            oldHasTRKI = HasTRKI;
            oldTRKINumber = TRKINumber;

            oldPhone = Phone;
            oldMobiles = Mobiles;

            oldCountryId = CountryId;

            oldRegionId = RegionId;

            oldCode = Code;
            oldCity = City;
            oldStreet = Street;
            oldHouse = House;
            oldKorpus = Korpus;
            oldFlat = Flat;

            oldCodeReal = CodeReal;
            oldCityReal = CityReal;
            oldStreetReal = StreetReal;
            oldHouseReal = HouseReal;
            oldKorpusReal = KorpusReal;
            oldFlatReal = FlatReal;

            if (HasCurrentEdication)
            {
                oldHasAccreditation = HasAccreditation;
                oldAccreditationDate = AccreditationDate;
                oldAccreditationNumber = AccreditationNumber;
                oldHasScholarship = HasScholarship;

                oldStudyBasisId = StudyBasisId;
                oldStudyFormId = StudyFormId;

                if (CurrentEducationSemesterId>0)
                    oldCurrentEducationSemesterId = CurrentEducationSemesterId;
                if (CurrentEducationStudyLevelId > 0)
                    oldCurrentEducationStudyLevelId = CurrentEducationStudyLevelId;
                oldCurrentLicenseProgramId = CurrentLicenseProgramId;
                oldCurrentObrazProgramId = CurrentObrazProgramId;
                oldProfileName = ProfileName;
            }
            if (HasDisorderInfo)
            {
                oldDisorderEducationName = DisorderEducationName;
                oldYearOfDisorder = YearOfDisorder;
                oldIsForIGA = IsForIGA;
            }
            if (HasReason)
            {
                oldReason = Reason;
            }
        }
        private void CopyToOldValues_Education()
        {
            oldCountryEducId = CountryEducId;
            oldSchoolTypeId = SchoolTypeId;
            oldSchoolCity = SchoolCity;
            oldEducationName = EducationName;
            oldEducationDocumentSeries = EducationDocumentSeries;
            oldEducationDocumentNumber = EducationDocumentNumber;
            oldRegionEduc = RegionEduc;
            if (ExitYear.HasValue)
                oldExitYear = ExitYear;
            if (EntryYear.HasValue)
                oldEntryYear = EntryYear;
            oldIsEqual = IsEqual;
            oldEqualDocumentNumber = EqualDocumentNumber;
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
            if(!(bool)dgvApps.Rows[e.RowIndex].Cells["IsViewed"].Value)
                e.CellStyle.BackColor = Color.LightBlue;
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
            else if (Check())
            {
                Lock();
                SaveCard();
                WriteAllChangesToHistory();
                WriteEducationChangesToHistory();
                CopyToOldValues();
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
                    Person.SecondName = SecondName;
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
                    CurrentEducation.StudyBasisId = StudyBasisId ?? 1;
                    CurrentEducation.StudyFormId = StudyFormId ?? 1;
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
                if (HasReason)
                {
                    #region Reason
                    var PersonReason = Person.PersonChangeStudyFormReason;
                    bool bHas = true;
                    if (PersonReason == null)
                    {
                        bHas = false;
                        PersonReason = new PersonChangeStudyFormReason();
                    }
                    PersonReason.Reason = Reason ?? "";
                    PersonReason.PersonId = _PersonId;
                    if (!bHas)
                        context.PersonChangeStudyFormReason.Add(PersonReason);
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
            btnSave.Enabled = true;
            btnHistory.Enabled = true; btnDisable.Enabled = true;
            lblFIO.Enabled = true;
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
                if (_isOpen)
                    SaveEducationDocument();
                _currentEducRow = dgvEducation.CurrentRow.Index;
                CurrEducationId = int.Parse(dgvEducation.CurrentRow.Cells["Id"].Value.ToString());
                ViewEducationInfo(CurrEducationId);
                WriteEducationChangesToHistory();
                CopyToOldValues_Education();
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
        private bool Check()
        {
            if (HasCurrentEdication)
                if (CurrentLicenseProgramId == null)
                {
                    MessageBox.Show("Выберите текущее направление обучения", "Ошибка");
                    return false;
                }
            return true;
        }

        private void WriteAllChangesToHistory()
        {
            string query = "INSERT INTO PersonHistory (PersonId, Action, OldValue, NewValue, Time, Owner) VALUES ('" + _PersonId.ToString() + "',  @Action, @OldValue, @NewValue, '" + DateTime.Now.ToString() + "', '" + System.Environment.UserName + "')";
            List<KeyValuePair<KeyValuePair<object, object>, string>> lst = new List<KeyValuePair<KeyValuePair<object, object>, string>>();
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldSurname, Surname), "Изменение фамилии"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldpersonName, personName), "Изменение имени"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldSecondName, SecondName), "Изменение отчества"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldBirthDate, BirthDate), "Изменение даты рождения"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldBirthPlace, BirthPlace), "Изменение места рождения"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldSex?"мужской":"женский", Sex?"мужской":"женский"), "Изменение пола"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(Util.NationalityList[oldNationalityId], Util.NationalityList[NationalityId]), "Изменение национальности"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldPassportSeries, PassportSeries), "Изменение серии паспорта"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldPassportNumber, PassportNumber), "Изменение номера паспорта"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldPassportAuthor, PassportAuthor), "Изменение орг-ции, выдавшей паспорт"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(Util.PassportTypeList[oldPassportTypeId], Util.PassportTypeList[PassportTypeId]), "Изменение типа паспорта"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldPassportDate, PassportDate), "Изменение даты выдачи паспорта"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldPassportCode, PassportCode), "Изменение кода паспорта"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldSNILS, SNILS), "Изменение СНИЛС"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldParents, Parents), "Изменение сведений о родителях"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldAddInfo, AddInfo), "Изменение доп. сведений"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldIsDisabled ? "да" : "нет", IsDisabled ? "да" : "нет"), "Изменение видимости в списке"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldHostelEduc ? "да, нуждается" : "нет, не нуждается", HostelEduc ? "да, нуждается" : "нет, не нуждается"), "Изменение требования общежития"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldHasTRKI ? "да" : "нет", HasTRKI ? "да" : "нет"), "Изменение наличие ТРКИ-сертификата"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldTRKINumber, TRKINumber), "Изменение номера ТРКИ-сертфиката"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldPhone, Phone), "Изменение номера телефона"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldMobiles, Mobiles), "Изменение номера мобильного телефона"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCountryId.HasValue ? Util.NationalityList[oldCountryId.Value]:"", CountryId.HasValue ? Util.NationalityList[CountryId.Value]:""), "Изменение страны проживания"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldRegionId.HasValue ? Util.RegionList[oldRegionId.Value] : "", RegionId.HasValue ? Util.RegionList[RegionId.Value]:""), "Изменение региона проживания"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCode, Code), "Изменение адреса: индекс"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCity, City), "Изменение адреса: город"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldStreet, Street), "Изменение адреса: улица"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldHouse, House), "Изменение адреса: дом"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldKorpus, Korpus), "Изменение адреса: корпус"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldFlat, Flat), "Изменение адреса: квартира"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCodeReal, CodeReal), "Изменение фактического адреса: индекс"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCityReal, CityReal), "Изменение фактического адреса: город"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldStreetReal, StreetReal), "Изменение фактического адреса: улица"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldHouseReal, HouseReal), "Изменение фактического адреса: дом"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldKorpusReal, KorpusReal), "Изменение фактического адреса: корпус"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldFlatReal, FlatReal), "Изменение фактического адреса: квартира"));

            if (HasCurrentEdication)
            {
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldHasAccreditation ? "да" : "нет", HasAccreditation ? "да" : "нет"), "Изменение наличия аккредитации"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldAccreditationDate, AccreditationDate), "Изменение даты аккредитации"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldAccreditationNumber, AccreditationNumber), "Изменение номера аккредитации"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldHasScholarship ? "да" : "нет", HasScholarship ? "да" : "нет"), "Изменение наличия стипендии"));

                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldStudyBasisId.HasValue ? Util.StudyBasisList[oldStudyBasisId.Value] : "", StudyBasisId.HasValue ? Util.StudyBasisList[StudyBasisId.Value]:""), "Изменение текущей основы обучения"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldStudyFormId.HasValue ? Util.StudyFormList[oldStudyFormId.Value] : "", StudyFormId.HasValue ? Util.StudyFormList[StudyFormId.Value] : ""), "Изменение текущей формы обучения"));

                if (CurrentEducationSemesterId > 0)
                    lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCurrentEducationSemesterId.HasValue ? Util.SemesterList[oldCurrentEducationSemesterId.Value] : "", CurrentEducationSemesterId.HasValue ? Util.SemesterList[CurrentEducationSemesterId.Value]:""), "Изменение текущего семестра"));
                if (CurrentEducationStudyLevelId > 0)
                    lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCurrentEducationStudyLevelId.HasValue ? Util.StudyLevelList[oldCurrentEducationStudyLevelId.Value] : "", CurrentEducationStudyLevelId.HasValue ? Util.StudyLevelList[CurrentEducationStudyLevelId.Value]:""), "Изменение текущего уровня образования"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCurrentLicenseProgramId.HasValue ? Util.LicenseProgramList[oldCurrentLicenseProgramId.Value] : "", CurrentLicenseProgramId.HasValue? Util.LicenseProgramList[CurrentLicenseProgramId.Value]:""), "Изменение текущего направления"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldCurrentObrazProgramId.HasValue ? Util.ObrazProgramList[oldCurrentObrazProgramId.Value] : "", CurrentObrazProgramId.HasValue ? Util.ObrazProgramList[CurrentObrazProgramId.Value]:""), "Изменение текущей образовательной программы"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldProfileName, ProfileName), "Изменение текущего профиля"));
            }
            if (HasDisorderInfo)
            {
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldYearOfDisorder, YearOfDisorder), "Изменение года отчисления"));
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldIsForIGA, IsForIGA), "Изменение условия \"Восстановление для ИГА\""));
            }
            if (HasReason)
            {
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldReason, Reason), "Изменение причины смены основы обучения"));
            }
            foreach (var x in lst)
                if (x.Key.Key == null && x.Key.Value == null)
                { }
                else if (x.Key.Key == null && x.Key.Value != null)
                    Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@OldValue", DBNull.Value }, { "@NewValue", x.Key.Value.ToString() }, { "@Action", x.Value } });
                else if (x.Key.Key != null && x.Key.Value == null)
                    Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@OldValue", x.Key.Key.ToString() }, { "@NewValue", DBNull.Value }, { "@Action", x.Value } });
                else if (x.Key.Key.ToString() != x.Key.Value.ToString())
                {
                    Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@OldValue", x.Key.Key.ToString() }, { "@NewValue", x.Key.Value.ToString() }, { "@Action", x.Value } });
                }
        }
        private void WriteEducationChangesToHistory()
        {
            string query = "INSERT INTO PersonHistory (PersonId, Action, OldValue, NewValue, Time, Owner) VALUES ('" + _PersonId.ToString() + "',  @Action, @OldValue, @NewValue, '" + DateTime.Now.ToString() + "', '" + System.Environment.UserName + "')";
            List<KeyValuePair<KeyValuePair<object, object>, string>> lst = new List<KeyValuePair<KeyValuePair<object, object>, string>>();
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(Util.NationalityList[oldCountryEducId], Util.NationalityList[CountryEducId]), "Изменение страны обучения"));

            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(Util.SchoolTypeList[oldSchoolTypeId],Util.SchoolTypeList[SchoolTypeId]), "Изменение типа образ.учрежд."));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldSchoolCity, SchoolCity), "Изменение нас. пункта образ.учрежд."));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldEducationName ,EducationName), "Изменение названия образ. учрежд."));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldEducationDocumentSeries ,EducationDocumentSeries), "Изменение серии документа об образовании"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldEducationDocumentNumber, EducationDocumentNumber), "Изменение номера документа об образовании"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldRegionEduc.HasValue ? Util.RegionList[oldRegionEduc.Value] : "", RegionEduc.HasValue ? Util.RegionList[RegionEduc.Value]:""), "Изменение региона образ.учрежд."));
            if (ExitYear.HasValue)
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldExitYear ,ExitYear), "Изменение года выпуска"));
            if (EntryYear.HasValue)
                lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldEntryYear ,EntryYear), "Изменение года поступления"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldIsEqual ? "есть" : "нет", IsEqual ? "есть" : "нет"), "Изменение параметра - св-во эквивалентности"));
            lst.Add(new KeyValuePair<KeyValuePair<object, object>, string>(new KeyValuePair<object, object>(oldEqualDocumentNumber ,EqualDocumentNumber), "Изменение номера св-ва эквивалентности"));


            foreach (var x in lst)
                if (x.Key.Key == null && x.Key.Value == null)
                { }
                else if (x.Key.Key == null && x.Key.Value != null)
                    Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@OldValue", DBNull.Value }, { "@NewValue", x.Key.Value.ToString() }, { "@Action", x.Value } });
                else if (x.Key.Key != null && x.Key.Value == null)
                    Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@OldValue", x.Key.Key.ToString() }, { "@NewValue", DBNull.Value }, { "@Action", x.Value } });
                else if (x.Key.Key.ToString() != x.Key.Value.ToString())
                {
                    Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@OldValue", x.Key.Key.ToString() }, { "@NewValue", x.Key.Value.ToString() }, { "@Action", x.Value } });
                }
        }
    }
}
