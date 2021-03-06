﻿using PriemForeignInspector.EDM;
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
    public partial class PersonRecoverCard : Form
    {
        private Guid _PersonId;
        private bool _isOpen;
        public UpdateHandler _handler;
        private int _AbitType = 3;

        public PersonRecoverCard(Guid id)
        {
            this.Text = "Восстановление в СПбГУ";
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

                var PersonDisorderInfo = p.PersonDisorderInfo;
                if (PersonDisorderInfo == null)
                    PersonDisorderInfo = new PriemForeignInspector.EDM.PersonDisorderInfo();

                DisorderEducationName = PersonDisorderInfo.EducationProgramName;
                YearOfDisorder = PersonDisorderInfo.YearOfDisorder;
                chbIsForIGA.Checked = PersonDisorderInfo.IsForIGA;

                //StudyLevel = r.Field<string>("StudyLevelRus");

                var PersonContacts = p.PersonContacts;
                if (PersonContacts == null)
                    PersonContacts = new PriemForeignInspector.EDM.PersonContacts();

                Phone = PersonContacts.Phone;
                Email = p.User.Email;
                Mobiles = PersonContacts.Mobiles;

                CountryId = PersonContacts.CountryId;
                RegionId = PersonContacts.RegionId ?? 1;

                CodeKLADR = PersonContacts.KladrCode;
                Code = PersonContacts.Code;
                City = PersonContacts.City;
                Street = PersonContacts.Street;
                House = PersonContacts.House;
                Korpus = PersonContacts.Korpus;
                Flat = PersonContacts.Flat;

                CodeReal = PersonContacts.CodeReal;
                CityReal = PersonContacts.CityReal;
                StreetReal = PersonContacts.StreetReal;
                HouseReal = PersonContacts.HouseReal;
                KorpusReal = PersonContacts.KorpusReal;
                FlatReal = PersonContacts.FlatReal;
                //------------------------------------------------------
                var PersonAddInfo = p.PersonAddInfo;
                if (PersonAddInfo == null)
                    PersonAddInfo = new PriemForeignInspector.EDM.PersonAddInfo();

                Parents = PersonAddInfo.Parents;
                AddInfo = PersonAddInfo.AddInfo;
                HostelEduc = PersonAddInfo.HostelEduc;
                IsDisabled = p.IsDisabled ?? false;
            }
            CheckBtnDisable();

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
                    Person.NationalityId = NationalityId;

                    Person.PassportSeries = PassportSeries;
                    Person.PassportNumber = PassportNumber;
                    Person.PassportAuthor = PassportAuthor;
                    Person.PassportDate = PassportDate;
                    Person.PassportCode = PassportCode;
                    Person.SNILS = SNILS;
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
                #region PersonDisorderInfo
                var PersonDisorderInfo = Person.PersonDisorderInfo;
                if (PersonDisorderInfo == null)
                {
                    PersonDisorderInfo = new PersonDisorderInfo();
                    PersonDisorderInfo.PersonId = _PersonId;
                    PersonDisorderInfo.EducationProgramName = DisorderEducationName;
                    PersonDisorderInfo.IsForIGA = chbIsForIGA.Checked;
                    PersonDisorderInfo.YearOfDisorder = YearOfDisorder;
                    context.PersonDisorderInfo.Add(PersonDisorderInfo);
                }
                else
                {
                    PersonDisorderInfo.EducationProgramName = DisorderEducationName;
                    PersonDisorderInfo.YearOfDisorder = YearOfDisorder;
                    PersonDisorderInfo.IsForIGA = chbIsForIGA.Checked;
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
                    PersonAddInfo.HostelEduc = HostelEduc;
                }
                #endregion

                PersonDisorderInfo.EducationProgramName = DisorderEducationName;
                PersonDisorderInfo.YearOfDisorder = YearOfDisorder;
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
        }

        private void chbHostelEducNo_CheckedChanged(object sender, EventArgs e)
        {
            chbHostelEducYes.Checked = !chbHostelEducNo.Checked;
        }
    }
}
