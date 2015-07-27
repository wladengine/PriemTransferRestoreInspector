using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriemForeignInspector
{
    partial class CardPerson
    {
        public bool IsDisabled { get; set; }

        #region PERSON
        public string FIO
        {
            get { return lblFIO.Text; }
            set
            {
                lblFIO.Text = value;
                this.Text = value + " - Карточка абитуриента";
            }
        }
        public string Surname
        {
            get { return tbSurname.Text; }
            set { tbSurname.Text = value; }
        }
        public string personName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }
        public string SecondName
        {
            get { return tbSecondName.Text; }
            set { tbSecondName.Text = value; }
        }
        public DateTime BirthDate
        {
            get { return dtpBirthDate.Value; }
            set { dtpBirthDate.Value = value; }
        }
        public string BirthPlace
        {
            get { return tbBirthPlace.Text; }
            set { tbBirthPlace.Text = value; }
        }
        public bool Sex
        {
            get { return rbMale.Checked; }
            set
            {
                if (!value)
                    rbFemale.Checked = true;
                else
                    rbMale.Checked = true;
            }
        }
        public int NationalityId
        {
            get { return (int)cbNationality.Id(); }
            set { cbNationality.Id(value); }
        }

        public int PassportTypeId
        {
            get { return (int)cbPassportType.Id(); }
            set { cbPassportType.Id(value); }
        }
        public string PassportSeries
        {
            get { return tbPassportSeries.Text; }
            set { tbPassportSeries.Text = value; }
        }
        public string PassportNumber
        {
            get { return tbPassportNumber.Text; }
            set { tbPassportNumber.Text = value; }
        }
        public string PassportAuthor
        {
            get { return tbPassportAuthor.Text; }
            set { tbPassportAuthor.Text = value; }
        }
        public string PassportCode
        {
            get { return tbPassportCode.Text; }
            set { tbPassportCode.Text = value; }
        }
        public string SNILS
        {
            get { return tbSNILS.Text; }
            set { tbSNILS.Text = value; }
        }
        public string PersonCode
        {
            get { return tbPersonCode.Text; }
            set { tbPersonCode.Text = value; }
        }

        public DateTime PassportDate
        {
            get { return dtpPassportDate.Value; }
            set
            {
                if (value == DateTime.MinValue)
                    dtpPassportDate.Checked = false;
                else if (value > System.Windows.Forms.DateTimePicker.MinimumDateTime)
                    dtpPassportDate.Value = value;
            }
        }

        #endregion

        #region PersonEDUCATION
        private int CurrEducationId { get; set; }
        public int SchoolTypeId
        {
            get { return (int)cbSchoolType.Id(); }
            set { cbSchoolType.Id(value); }
        }
        public string SchoolCity
        {
            get { return tbEducationSchoolCity.Text.Trim(); }
            set { tbEducationSchoolCity.Text = value.Trim(); }
        }
        public string EducationName
        {
            get { return tbEducationName.Text; }
            set { tbEducationName.Text = value; }
        }
        public int CountryEducId
        {
            get { return (int)cbCountryEduc.Id(); }
            set { cbCountryEduc.Id(value); }
        }
        public int? RegionEduc
        {
            get { return (int?)cbRegionEduc.Id(); }
            set { cbRegionEduc.Id(value); }
        }
        public int? StudyFormId
        {
            get { return (int?)cbStudyForm.Id(); }
            set { cbStudyForm.Id(value); }
        }
        public int? StudyBasisId
        {
            get { return (int?)cbStudyBasis.Id(); }
            set { cbStudyBasis.Id(value); }
        }
        public int? ExitYear
        {
            get
            {
                int ret;
                if (!int.TryParse(tbExitYear.Text, out ret))
                    return null;
                return ret;
            }
            set { tbExitYear.Text = value.HasValue ? value.Value.ToString() : ""; }
        }
        public int? EntryYear
        {
            get
            {
                int ret;
                if (!int.TryParse(tbEntryYear.Text, out ret))
                    return null;
                return ret;
            }
            set { tbEntryYear.Text = value.HasValue ? value.Value.ToString() : ""; }
        }
        public string EducationDocumentSeries
        {
            get { return tbSeries.Text; }
            set { tbSeries.Text = value; }
        }
        public string EducationDocumentNumber
        {
            get { return tbNumber.Text; }
            set { tbNumber.Text = value; }
        }
        private bool IsEqual
        {
            get { return chbIsEqual.Checked; }
            set { chbIsEqual.Checked = value; }
        }
        public string EqualDocumentNumber
        {
            get { return tbEqualityDocumentNumber.Text.Trim(); }
            set { tbEqualityDocumentNumber.Text = value; }
        }
        #endregion

        #region PersonCONTACTS
        public string Phone
        {
            get { return tbPhone.Text; }
            set { tbPhone.Text = value; }
        }
        public string Email
        {
            get { return tbEmail.Text; }
            set { tbEmail.Text = value; }
        }
        public string Mobiles
        {
            get { return tbMobiles.Text; }
            set { tbMobiles.Text = value; }
        }
        public int? CountryId
        {
            get { return (int?)cbCountry.Id(); }
            set { cbCountry.Id(value); }
        }
        public int? RegionId
        {
            get { return (int?)cbRegion.Id(); }
            set { cbRegion.Id(value); }
        }
        public string Code
        {
            get { return tbCode.Text; }
            set { tbCode.Text = value; }
        }
        public string CodeKLADR
        {
            get { return tbCodeKladr.Text; }
            set { tbCodeKladr.Text = value; }
        }
        public string City
        {
            get { return tbCity.Text; }
            set { tbCity.Text = value; }
        }
        public string Street
        {
            get { return tbStreet.Text; }
            set { tbStreet.Text = value; }
        }
        public string House
        {
            get { return tbHouse.Text; }
            set { tbHouse.Text = value; }
        }
        public string Korpus
        {
            get { return tbKorpus.Text; }
            set { tbKorpus.Text = value; }
        }
        public string Flat
        {
            get { return tbFlat.Text; }
            set { tbFlat.Text = value; }
        }
        public string CodeReal
        {
            get { return tbCodeReal.Text; }
            set { tbCodeReal.Text = value; }
        }
        public string CityReal
        {
            get { return tbCityReal.Text; }
            set { tbCityReal.Text = value; }
        }
        public string StreetReal
        {
            get { return tbStreetReal.Text; }
            set { tbStreetReal.Text = value; }
        }
        public string HouseReal
        {
            get { return tbHouseReal.Text; }
            set { tbHouseReal.Text = value; }
        }
        public string KorpusReal
        {
            get { return tbKorpusReal.Text; }
            set { tbKorpusReal.Text = value; }
        }
        public string FlatReal
        {
            get { return tbFlatReal.Text; }
            set { tbFlatReal.Text = value; }
        }
        #endregion

        #region PersonCURRENT_EDUCATION
        public int? CurrentEducationStudyLevelId
        {
            get { return (int?)ComboServ.GetComboIdInt(cbCurrentEducationStudyLevel); }
            set { ComboServ.SetComboId(cbCurrentEducationStudyLevel, value); }
        }
        public int? CurrentEducationSemesterId
        {
            get { return (int?)ComboServ.GetComboIdInt(cbCurrentEducationSemester); }
            set { ComboServ.SetComboId(cbCurrentEducationSemester, value); }
        }
        public int? CurrentLicenseProgramId
        {
            get { return (int?)cbLicenseProgram.Id(); }
            set { cbLicenseProgram.Id(value ?? 1); }
        }
        public string ProfileName
        {
            get { return tbCurrentProfile.Text.Trim(); }
            set { tbCurrentProfile.Text = value.Trim(); }
        }
        public int? CurrentObrazProgramId
        {
            get { return (int?)cbCurrentObrazProgram.Id(); }
            set { cbCurrentObrazProgram.Id(value ?? 1); }
        }
        public string AccreditationNumber
        {
            get { return tbAccreditationNumber.Text.Trim(); }
            set { tbAccreditationNumber.Text = value; }
        }
        public DateTime? AccreditationDate
        {
            get
            {
                if (dtpAccreditationDate.Checked)
                    return dtpAccreditationDate.Value;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                {
                    dtpAccreditationDate.Checked = true;
                    dtpAccreditationDate.Value = value.Value;
                }
                else
                    dtpAccreditationDate.Checked = false;
            }
        }
        public bool HasAccreditation
        {
            get { return chbHasAccreditation.Checked; }
            set { chbHasAccreditation.Checked = value; }
        }
        public bool HasScholarship
        {
            get { return chbHasScholarship.Checked; }
            set { chbHasScholarship.Checked = value; }
        }
        #endregion

        #region PersonADDITIONAL_INFO
        public string Parents
        {
            get { return tbParents.Text.Trim(); }
            set { tbParents.Text = value; }
        }
        public string AddInfo
        {
            get { return tbAddInfo.Text.Trim(); }
            set { tbAddInfo.Text = value; }
        }
        public bool HostelEduc
        {
            get { return chbHostelEducYes.Checked; }
            set
            {
                chbHostelEducYes.Checked = value;
                chbHostelEducNo.Checked = !value;
            }
        }
        public bool HasTRKI
        {
            get { return chbTRKI.Checked; }
            set { chbTRKI.Checked = value; }
        }
        public string TRKINumber
        {
            get { return tbTRKINumber.Text.Trim(); }
            set { tbTRKINumber.Text = value.Trim(); }
        }
        #endregion

        #region PersonDISORDER_info
        public string YearOfDisorder
        {
            get { return tbDisorderYear.Text.Trim(); }
            set { tbDisorderYear.Text = value; }
        }
        public string DisorderEducationName
        {
            get { return tbEducationDisorderName.Text.Trim(); }
            set { tbEducationDisorderName.Text = value; }
        }
        private bool IsForIGA
        {
            get { return chbIsForIGA.Checked; }
            set
            {
                chbIsForIGA.Checked = value;
            }
        }
        #endregion
    }
}
