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
    public partial class PersonTransferForeignCard : Form
    {
        private Guid _PersonId;
        private bool _isOpen;
        public UpdateHandler _handler;
        private int _AbitType = 2;
        List<PersonEducationDocument> PersonEducDocument;
        int _currentEducRow;

        public PersonTransferForeignCard(Guid id)
        {
            this.Text = "Перевода в СПБГУ из Иностранного ВУЗа";
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
            FillComboSchoolType();
            FillComboCountryEduc();
            FillComboCurrentEducationStudyLevel();
            FillComboCurrentEducationSemester();
            FillComboCurrentStudyForm();
            FillComboCurrentStudyBasis();
            FillComboCurrentLicenseProgram();
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
            string query = "SELECT DISTINCT StudyLevelId AS Id, StudyLevelName AS Name FROM Entry ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbCurrentEducationStudyLevel.AddItems(bind);
        }
        private void FillComboCurrentEducationSemester()
        {
            string query = "SELECT Id, Name FROM Semester ORDER BY 1";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbCurrentEducationSemester.AddItems(bind);
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

                var PersonEducationDocument = p.PersonEducationDocument.First();
                if (PersonEducationDocument == null)
                    PersonEducationDocument = new PriemForeignInspector.PersonEducationDocument();

                /*int exYear;
                if (!int.TryParse(PersonEducationDocument.SchoolExitYear, out exYear))
                    ExitYear = null;
                else
                    ExitYear = exYear;*/

                
                //EducationName = PersonEducationDocument.SchoolName;
                //SchoolTypeId = PersonEducationDocument.SchoolTypeId ?? 1;

               //EducationDocumentSeries = PersonEducationDocument.Series;
               //EducationDocumentNumber = PersonEducationDocument.Number;

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

                AccreditationDate = PersonCurrentEducation.AccreditationDate;
                AccreditationNumber = PersonCurrentEducation.AccreditationNumber;

                IsEqual = PersonEducationDocument.IsEqual;
                EqualityDocumentNumber = PersonEducationDocument.EqualDocumentNumber;
                CountryEducId = PersonEducationDocument.CountryEducId;
                HasAccreditation = PersonCurrentEducation.HasAccreditation;
                HasScholarship = PersonCurrentEducation.HasScholarship;

                CurrentEducationSemesterId = PersonCurrentEducation.SemesterId;
                CurrentEducationStudyLevelId = PersonCurrentEducation.StudyLevelId;

                //------------------------------------------------------
                var PersonAddInfo = p.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PriemForeignInspector.PersonAddInfo();

                MaritalStatus = PersonAddInfo.MaritalStatus;
                SocialStatus = PersonAddInfo.SocialStatus;

                Parents = PersonAddInfo.Parents;
                AddInfo = PersonAddInfo.AddInfo;
                HostelEduc = PersonAddInfo.HostelEduc;
                IsDisabled = p.IsDisabled ?? false;
            }
            CheckBtnDisable();
            FillEducationData(_PersonId);
            int ContryEduc = (int)Util.BDC.GetValue(@"Select top 1 CountryEducId from PersonEducationDocument where 
								PersonId=@Id and SchoolTypeId = 4  order by Id", new Dictionary<string, object>() { { "@Id", _PersonId } });


            string query = @"SELECT [Application].Id,[Application].CommitId, LicenseProgramName AS 'Направление', ObrazProgramName AS 'Образовательная программа', 
            ProfileName AS 'Профиль', SemesterId as 'Семестр',  IsCommited, IsDeleted, Enabled, 
            IsApprovedByComission,
            case when (Application.SecondTypeId =2) 
				then (
					" + (ContryEduc == 193 ?
                      @"select [AbiturientType].[Description] from AbiturientType where Id = 3" :
                     @"select [AbiturientType].[Description] from AbiturientType where Id = 4 ") +
                @") else  (Select [AbiturientType].[Description] from AbiturientType where AppSecondTypeId = Application.SecondTypeId) end  AS 'Тип' 
            FROM [Application] 
            INNER JOIN Entry ON Entry.Id = [Application].EntryId 
            WHERE PersonId=@Id  and IsCommited = 1
            --AND Enabled='True'  
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
                var lstVals = (from ed in context.PersonEducationDocument
                               join he in context.PersonHighEducationInfo on ed.Id equals he.EducationDocumentId into gj
                               from heduc in gj.DefaultIfEmpty()
                               where ed.Id == EducId
                               select new
                               {
                                   ed.Id,
                                   ed.Series,
                                   ed.Number,
                                   ed.SchoolNum,
                                   ed.SchoolTypeId,
                                   ed.SchoolName,
                                   ed.CountryEducId,
                                   ed.SchoolExitYear,
                                   ed.RegionEducId,
                                   heduc.EntryYear,
                               }).First();
                CountryEducId = lstVals.CountryEducId;
                SchoolTypeId = lstVals.SchoolTypeId;
                EducationName = lstVals.SchoolName + (!String.IsNullOrEmpty(lstVals.SchoolNum) ? (SchoolTypeId == 1 ? "(" + lstVals.SchoolNum.ToString() + ")" : "") : "");
                EducationDocumentSeries = lstVals.Series;
                EducationDocumentNumber = lstVals.Number;
                int ex_year;
                if (int.TryParse(lstVals.SchoolExitYear, out ex_year))
                    ExitYear = ex_year;
                else
                    ExitYear = null;
                if (lstVals.EntryYear.HasValue)
                    EntryYear = lstVals.EntryYear.Value;
                else
                    EntryYear = null;
                RegionEduc = lstVals.RegionEducId;
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
            Util.OpenAbiturientCard(this.Owner, id, 4);
        }
        private void dgvApps_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           /* bool Enabled = (bool)dgvApps.Rows[e.RowIndex].Cells["Enabled"].Value;
            if (!Enabled)
                e.CellStyle.BackColor = Color.Red;
            bool IsCommited = (bool)dgvApps.Rows[e.RowIndex].Cells["IsCommited"].Value;
            bool IsDeleted = (bool)dgvApps.Rows[e.RowIndex].Cells["IsDeleted"].Value;
            if ((!IsCommited)||(IsDeleted))
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
                if (!CheckFields())
                    return;
                Lock();
                SaveCard();
                if (_handler != null)
                    _handler();
            }
        }
        private bool CheckFields()
        {
            if (_AbitType == 2)
            {
                if (!CurrentLicenseProgramId.HasValue)
                {
                    MessageBox.Show("Выберите текущее направление обучения");
                    return false;
                }
            }
            return true;
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

                    PersonContacts.Phone = Phone;
                    PersonContacts.Mobiles = Mobiles;
                    PersonContacts.CountryId = CountryId ?? Util.CountryRussiaId;
                    PersonContacts.RegionId = RegionId;
                }
                #endregion
                #region PersonCurrentEducation
                var PersonCurrentEducation = Person.PersonCurrentEducation;
                if (PersonCurrentEducation == null)
                {
                    PersonCurrentEducation = new PersonCurrentEducation();
                    PersonCurrentEducation.PersonId = _PersonId;
                    PersonCurrentEducation.AccreditationDate = AccreditationDate;
                    PersonCurrentEducation.AccreditationNumber = AccreditationNumber;
                    PersonCurrentEducation.EducName = "";
                    PersonCurrentEducation.HasAccreditation = HasAccreditation;
                    PersonCurrentEducation.HasScholarship = HasScholarship;
                    PersonCurrentEducation.SemesterId = CurrentEducationSemesterId ?? 3;
                    PersonCurrentEducation.StudyLevelId = CurrentEducationStudyLevelId ?? 16;
                    PersonCurrentEducation.LicenseProgramId = CurrentLicenseProgramId ?? 1;
                    context.PersonCurrentEducation.Add(PersonCurrentEducation);
                }
                else
                {
                    PersonCurrentEducation.AccreditationDate = AccreditationDate;
                    PersonCurrentEducation.AccreditationNumber = AccreditationNumber;
                    PersonCurrentEducation.EducName = "";
                    PersonCurrentEducation.EducTypeId = 1;
                    PersonCurrentEducation.HasAccreditation = HasAccreditation;
                    PersonCurrentEducation.HasScholarship = HasScholarship;
                    PersonCurrentEducation.SemesterId = CurrentEducationSemesterId ?? 3;
                    PersonCurrentEducation.StudyLevelId = CurrentEducationStudyLevelId ?? 16;
                    PersonCurrentEducation.LicenseProgramId = CurrentLicenseProgramId ?? 1;
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
                    PersonAddInfo.AddInfo = AddInfo;
                    PersonAddInfo.Parents = Parents;
                    PersonAddInfo.MaritalStatus = MaritalStatus;
                    PersonAddInfo.SocialStatus = SocialStatus;
                    PersonAddInfo.HostelEduc = HostelEduc;
                }
                #endregion

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

        private void btnEducationSave_Click(object sender, EventArgs e)
        {
            if (!_isOpen)
                return;
            SaveEducationDocument();
        }
        private void SaveEducationDocument()
        {
            using (OnlinePriem2012Entities context = new OnlinePriem2012Entities())
            {
                PersonEducationDocument Doc = context.PersonEducationDocument.Where(x => x.Id == CurrEducationId).Select(x => x).First();
                int ind = PersonEducDocument.FindIndex(x => x.Id == CurrEducationId);
                if (ind >= 0)
                {
                    PersonEducDocument[ind].SchoolTypeId = SchoolTypeId;
                    Doc.SchoolTypeId = SchoolTypeId;
                    PersonEducDocument[ind].SchoolName = EducationName;
                    Doc.SchoolName = EducationName;
                    PersonEducDocument[ind].SchoolExitYear = ExitYear.ToString();
                    Doc.SchoolExitYear = ExitYear.ToString();
                    PersonEducDocument[ind].CountryEducId = CountryEducId;
                    Doc.SchoolName = EducationName;
                    PersonEducDocument[ind].RegionEducId = RegionEduc.Value;
                    Doc.RegionEducId = RegionEduc.Value;
                    PersonEducDocument[ind].Series = EducationDocumentSeries;
                    Doc.Series = EducationDocumentSeries;
                    PersonEducDocument[ind].Number = EducationDocumentNumber;
                    Doc.Number = EducationDocumentNumber;
                    PersonEducDocument[ind].IsEqual = IsEqual;
                    Doc.IsEqual = IsEqual;
                    PersonEducDocument[ind].EqualDocumentNumber = EqualityDocumentNumber;
                    Doc.EqualDocumentNumber = EqualityDocumentNumber;

                    if (SchoolTypeId == 4)
                    {
                        PersonHighEducationInfo HEduc = context.PersonHighEducationInfo.Where(x => x.EducationDocumentId == CurrEducationId).Select(x => x).FirstOrDefault();
                        if (HEduc == null)
                            HEduc = new PersonHighEducationInfo();
                        HEduc.EntryYear = EntryYear;
                    }

                    dgvEducation["School", ind].Value = EducationName;
                    dgvEducation["Series", ind].Value = EducationDocumentSeries;
                    dgvEducation["Num", ind].Value = EducationDocumentNumber;
                    context.SaveChanges();
                }
            }
        }

        private void dgvEducation_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvEducation.CurrentCell == null)
                return;

            if (_currentEducRow != dgvEducation.CurrentRow.Index)
            {
                SaveEducationDocument();
                _currentEducRow = dgvEducation.CurrentRow.Index;
                ViewEducationInfo(int.Parse(dgvEducation.CurrentRow.Cells["Id"].Value.ToString()));
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
    }
}
