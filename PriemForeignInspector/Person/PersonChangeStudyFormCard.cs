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
    public partial class PersonChangeStudyFormCard : Form
    {
        private Guid _PersonId;
        private bool _isOpen;
        public UpdateHandler _handler;

        public PersonChangeStudyFormCard(Guid id)
        {
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
            FillComboSemester();
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
        private void FillComboLicenseProgram()
        {
            string query = "SELECT LicenseProgramId AS Id, LicenseProgramName AS Name FROM [Entry] WHERE StudyLevelId=@StudyLevelId";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@StudyLevelId", StudyLevelId } });
            List<KeyValuePair<object, string>> bind =
                (from DataRow rw in tbl.Rows
                 select new KeyValuePair<object, string>(rw.Field<int>("Id"), rw.Field<string>("Name"))).ToList();
            cbLicenseProgram.AddItems(bind);
        }
        private void FillComboSemester()
        {
            string query = "SELECT Id, Name FROM Semester";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
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
                //------------------------------------------------------

                var PersonCurrentEducation = p.PersonCurrentEducation;
                if (PersonCurrentEducation == null)
                    PersonCurrentEducation = new PersonCurrentEducation();

                SemesterId = PersonCurrentEducation.SemesterId.Value;
                StudyLevelId = PersonCurrentEducation.StudyLevelId.Value;
                if (PersonCurrentEducation.LicenseProgramId.HasValue)
                    LicenseProgramId = PersonCurrentEducation.LicenseProgramId.Value;
                ProfileName = PersonCurrentEducation.ProfileName;


                //StudyLevel = r.Field<string>("StudyLevelRus");

                var PersonContacts = p.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PriemForeignInspector.PersonContacts();

                Phone = PersonContacts.Phone;
                Email = p.User.Email;
                CountryId = PersonContacts.CountryId ?? Util.CountryRussiaId;
                RegionId = PersonContacts.RegionId ?? 1;
                Code = PersonContacts.Code;
                City = PersonContacts.City;
                Street = PersonContacts.Street;
                House = PersonContacts.House;
                Korpus = PersonContacts.Korpus;
                Flat = PersonContacts.Flat;
                //------------------------------------------------------


                var PersonAddInfo = p.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PriemForeignInspector.PersonAddInfo();

                Parents = PersonAddInfo.Parents;
                AddInfo = PersonAddInfo.AddInfo;

                IsDisabled = p.IsDisabled ?? false;
            }
            CheckBtnDisable();

            string query = @"SELECT [Application].Id, LicenseProgramName AS 'Направление', ObrazProgramName AS 'Образовательная программа', ProfileName AS 'Профиль', Enabled FROM [Application]
            INNER JOIN Entry ON Entry.Id = [Application].EntryId 
            WHERE PersonId=@Id --AND Enabled='True'";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", _PersonId } });
            dgvApps.DataSource = tbl;
            dgvApps.Columns["Id"].Visible = false;
            dgvApps.Columns["Enabled"].Visible = false;
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
            Util.OpenAbiturientCard(this.Owner, id);
        }
        private void dgvApps_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            bool Enabled = (bool)dgvApps.Rows[e.RowIndex].Cells["Enabled"].Value;
            if (!Enabled)
                e.CellStyle.BackColor = Color.Red;
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

                Person.NationalityId = NationalityId;

                PersonContacts.Code = Code;
                PersonContacts.City = City;
                PersonContacts.Street = Street;
                PersonContacts.House = House;
                PersonContacts.Korpus = Korpus;
                PersonContacts.Flat = Flat;

                PersonContacts.Phone = Phone;
                //PersonContacts.Mobiles = Mobiles;
                PersonContacts.CountryId = CountryId;
                PersonContacts.RegionId = RegionId;

                PersonCurrentEducation.SemesterId = SemesterId;
                PersonCurrentEducation.StudyLevelId = StudyLevelId;
                PersonCurrentEducation.LicenseProgramId = LicenseProgramId;
                PersonCurrentEducation.ProfileName = ProfileName;

                PersonAddInfo.AddInfo = AddInfo;
                PersonAddInfo.Parents = Parents;

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
            FillComboLicenseProgram();
        }
    }
}
