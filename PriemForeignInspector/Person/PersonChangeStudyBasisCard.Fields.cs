using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriemForeignInspector
{
    partial class PersonChangeStudyBasisCard
    {
        public string FIO
        {
            get
            {
                return lblFIO.Text;
            }
            set
            {
                lblFIO.Text = value;
                this.Text = value + " - Карточка абитуриента";
            }
        }
        public string Surname
        {
            get
            {
                return tbSurname.Text;
            }
            set
            {
                tbSurname.Text = value;
            }
        }
        public string personName
        {
            get
            {
                return tbName.Text;
            }
            set
            {
                tbName.Text = value;
            }
        }
        public string SecondName
        {
            get
            {
                return tbSecondName.Text;
            }
            set
            {
                tbSecondName.Text = value;
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return dtpBirthDate.Value;
            }
            set
            {
                dtpBirthDate.Value = value;
            }
        }
        public string BirthPlace
        {
            get
            {
                return tbBirthPlace.Text;
            }
            set
            {
                tbBirthPlace.Text = value;
            }
        }
        public bool Sex
        {
            get
            {
                return rbMale.Checked;
            }
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
            get
            {
                return (int)cbPassportType.Id();
            }
            set
            {
                cbPassportType.Id(value);
            }
        }
        public string PassportSeries
        {
            get
            {
                return tbPassportSeries.Text;
            }
            set
            {
                tbPassportSeries.Text = value;
            }
        }
        public string PassportNumber
        {
            get
            {
                return tbPassportNumber.Text;
            }
            set
            {
                tbPassportNumber.Text = value;
            }
        }
        public string PassportAuthor
        {
            get
            {
                return tbPassportAuthor.Text;
            }
            set
            {
                tbPassportAuthor.Text = value;
            }
        }
        public string PassportCode
        {
            get
            {
                return tbPassportCode.Text;
            }
            set
            {
                tbPassportCode.Text = value;
            }
        }
        public string PersonCode
        {
            get
            {
                return tbPersonCode.Text;
            }
            set
            {
                tbPersonCode.Text = value;
            }
        }
        public string SNILS
        {
            get
            {
                return tbSNILS.Text;
            }
            set
            {
                tbSNILS.Text = value;
            }
        }

        public DateTime PassportDate
        {
            get
            {
                return dtpPassportDate.Value;
            }
            set
            {
                dtpPassportDate.Value = (value == DateTime.MinValue ? DateTime.Now : value);
            }
        }

        public string ProfileName
        {
            get { return tbProfile.Text.Trim(); }
            set { tbProfile.Text = value; }
        }
        public int? StudyLevelId
        {
            get { return (int?)cbStudyLevel.Id(); }
            set { cbStudyLevel.Id(value); }
        }
        public int? SemesterId
        {
            get { return (int?)cbSemester.Id(); }
            set { cbSemester.Id(value); }
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

        public int? LicenseProgramId
        {
            get { return (int?)cbLicenseProgram.Id(); }
            set { cbLicenseProgram.Id(value); }
        }
        public int? ObrazProgramId
        {
            get { return (int?)cbObrazProgram.Id(); }
            set { cbObrazProgram.Id(value); }
        }
        public string Phone
        {
            get
            {
                return tbPhone.Text;
            }
            set
            {
                tbPhone.Text = value;
            }
        }
        public string Email
        {
            get
            {
                return tbEmail.Text;
            }
            set
            {
                tbEmail.Text = value;
            }
        }
        public string Mobiles
        {
            get
            {
                return tbMobiles.Text;
            }
            set
            {
                tbMobiles.Text = value;
            }
        }


        public int? CountryId
        {
            get
            {
                return (int?)cbCountry.Id();
            }
            set
            {
                cbCountry.Id(value);
            }
        }
        public int? RegionId
        {
            get
            {
                return (int?)cbRegion.Id();
            }
            set
            {
                cbRegion.Id(value);
            }
        }
        public string CodeKLADR
        {
            get
            {
                return tbCodeKladr.Text;
            }
            set
            {
                tbCodeKladr.Text = value;
            }
        }

        public string Code
        {
            get
            {
                return tbCode.Text;
            }
            set
            {
                tbCode.Text = value;
            }
        }
        public string City
        {
            get
            {
                return tbCity.Text;
            }
            set
            {
                tbCity.Text = value;
            }
        }
        public string Street
        {
            get
            {
                return tbStreet.Text;
            }
            set
            {
                tbStreet.Text = value;
            }
        }
        public string House
        {
            get
            {
                return tbHouse.Text;
            }
            set
            {
                tbHouse.Text = value;
            }
        }
        public string Korpus
        {
            get
            {
                return tbKorpus.Text;
            }
            set
            {
                tbKorpus.Text = value;
            }
        }
        public string Flat
        {
            get
            {
                return tbFlat.Text;
            }
            set
            {
                tbFlat.Text = value;
            }
        }

        public string CodeReal
        {
            get
            {
                return tbCodeReal.Text;
            }
            set
            {
                tbCodeReal.Text = value;
            }
        }
        public string CityReal
        {
            get
            {
                return tbCityReal.Text;
            }
            set
            {
                tbCityReal.Text = value;
            }
        }
        public string StreetReal
        {
            get
            {
                return tbStreetReal.Text;
            }
            set
            {
                tbStreetReal.Text = value;
            }
        }
        public string HouseReal
        {
            get
            {
                return tbHouseReal.Text;
            }
            set
            {
                tbHouseReal.Text = value;
            }
        }
        public string KorpusReal
        {
            get
            {
                return tbKorpusReal.Text;
            }
            set
            {
                tbKorpusReal.Text = value;
            }
        }
        public string FlatReal
        {
            get
            {
                return tbFlatReal.Text;
            }
            set
            {
                tbFlatReal.Text = value;
            }
        }

        public bool IsDisabled { get; set; }

        public bool HostelEduc
        {
            get { return chbHostelEducYes.Checked; }
            set
            {
                chbHostelEducYes.Checked = value;
                chbHostelEducNo.Checked = !value;
            }
        }

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
        public string ChangeStudyBasisReason
        {
            get { return tbChangeStudyBasisReason.Text.Trim(); }
            set { tbChangeStudyBasisReason.Text = value; }
        }
    }
}
