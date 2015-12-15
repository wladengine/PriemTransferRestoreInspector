using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using EducServLib;

namespace PriemForeignInspector
{
    partial class AbiturientCard
    {
        public int? SemesterId
        {
            get
            {
                return ComboServ.GetComboIdInt(cbSemester);
            }
            set
            {
                ComboServ.SetComboId(cbSemester, value);
            }
        }
        public int? LicenseProgram
        {
            get
            {
                return ComboServ.GetComboIdInt(cbLicenseProgram);
            }
            set
            {
                ComboServ.SetComboId(cbLicenseProgram, value);
                //cbLicenseProgram.Id(value);
            }
        }
        public int? ObrazProgram
        {
            get
            {
                return ComboServ.GetComboIdInt(cbObrazProgram);
                //return (int)cbObrazProgram.Id();
            }
            set
            {
                ComboServ.SetComboId(cbObrazProgram, value);
                //cbObrazProgram.Id(value);
            }
        }
        public int? ProfileId
        {
            get
            {
                return ComboServ.GetComboIdInt(cbProfile);
            }
            set
            {
                ComboServ.SetComboId(cbProfile, value.ToString());
            }
        }
        public int? Faculty
        {
            get
            {
                 return ComboServ.GetComboIdInt(cbFaculty);
               //return (int)cbFaculty.Id();
            }
            set
            {
                ComboServ.SetComboId(cbFaculty, value);
                //cbFaculty.Id(value);
            }
        }
        public int? StudyForm
        {
            get
            {
                return ComboServ.GetComboIdInt(cbStudyForm);
                //return (int?)cbStudyForm.Id();
            }
            set
            {
                ComboServ.SetComboId(cbStudyForm, value);
                //cbStudyForm.Id(value);
            }
        }
        public int? StudyBasis
        {
            get
            {
                return ComboServ.GetComboIdInt(cbStudyBasis);
                //return (int)cbStudyBasis.Id();
            }
            set
            {
                ComboServ.SetComboId(cbStudyBasis, value);
                //cbStudyBasis.Id(value);
            }
        }
        public string FIO
        {
            get
            {
                return lblFIO.Text;
            }
            set
            {
                lblFIO.Text = value;
                this.Text = value + " - Заявление";
            }
        }
        public int? StudyLevel
        {
            get
            {
                return ComboServ.GetComboIdInt(cbStudyLevel);
                //return (int)cbStudyLevel.Id();
            }
            set
            {
                ComboServ.SetComboId(cbStudyLevel, value);
                //cbStudyLevel.Id(value);
            }
        }
        public string Status
        {
            get
            {
                return tbAppStatus.Text;
            }
            set
            {
                tbAppStatus.Text = value;
            }
        }
 
        public string Email { get; set; }
    }
}
