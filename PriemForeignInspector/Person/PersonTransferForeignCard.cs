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
            FillComboCurrentEducationType();
            FillComboCurrentEducationStudyLevel();
            FillComboCurrentEducationSemester();
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
        private void FillComboCurrentEducationType()
        {
            string query = "SELECT Id, Name FROM SchoolTypeAll ORDER BY 2";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbCurrentEducationType.AddItems(bind);
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

                CurrentEducationName = PersonEducationDocument.SchoolName;
                CurrentEducationTypeId = PersonEducationDocument.SchoolTypeId;

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
            WHERE PersonId=@Id   and IsCommited = 1
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

                var PersonEducationDocument = Person.PersonEducationDocument.FirstOrDefault();
                if (PersonEducationDocument == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonEducationDocument");
                    return;
                }

                var PersonHighEducationInfo = Person.PersonEducationDocument.First().PersonHighEducationInfo;
                if (PersonHighEducationInfo == null)
                {
                    MessageBox.Show("Ошибка при получении данных PersonHighEducationInfo");
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

                PersonCurrentEducation.AccreditationDate = AccreditationDate;
                PersonCurrentEducation.AccreditationNumber = AccreditationNumber;
                PersonCurrentEducation.EducName = CurrentEducationName;
                PersonCurrentEducation.EducTypeId = CurrentEducationTypeId ?? 4;
                PersonCurrentEducation.HasAccreditation = HasAccreditation;
                PersonCurrentEducation.HasScholarship = HasScholarship;
                PersonCurrentEducation.SemesterId = CurrentEducationSemesterId ?? 3;
                PersonCurrentEducation.StudyLevelId = CurrentEducationStudyLevelId ?? 16;

                PersonEducationDocument.SchoolExitYear = ExitYear.HasValue ? ExitYear.Value.ToString() : null;
                PersonHighEducationInfo.ExitYear = ExitYear ?? (DateTime.Now.Year - 1);

                PersonAddInfo.AddInfo = AddInfo;
                PersonAddInfo.Parents = Parents;
                PersonAddInfo.MaritalStatus = MaritalStatus;
                PersonAddInfo.SocialStatus = SocialStatus;
                PersonAddInfo.HostelEduc = HostelEduc;

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

            //tbSurname.Enabled = _isOpen;
            //tbName.Enabled = _isOpen;
            //tbPassportSeries.Enabled = _isOpen;
            //tbPassportNumber.Enabled = _isOpen;
            //tbPassportAuthor.Enabled = _isOpen;
            //tbPassportCode.Enabled = _isOpen;
            //tbPassportCode.Enabled = _isOpen;
            //cbPassportType.Enabled = _isOpen;
            //tbBirthPlace.Enabled = _isOpen;
            //dtpBirthDate.Enabled = _isOpen;
            //dtpPassportDate.Enabled = _isOpen;

            //tbCode.Enabled = _isOpen;
            //tbCity.Enabled = _isOpen;
            //tbStreet.Enabled = _isOpen;
            //tbHouse.Enabled = _isOpen;
            //tbKorpus.Enabled = _isOpen;
            //tbFlat.Enabled = _isOpen;

            //cbSchoolType.Enabled = _isOpen;
            //cbCountryEduc.Enabled = _isOpen;

            //tbEducationName.Enabled = _isOpen;
            //tbExitYear.Enabled = _isOpen;


            //tbPhone.Enabled = _isOpen;

            //cbNationality.Enabled = _isOpen;



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
    }
}
