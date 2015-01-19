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
    public partial class OlympList : Form
    {
        private int iNowYear = 2013;
        private bool isOddSemester = true;

        public OlympList()
        {
            InitializeComponent();
        }

        private void btnCreateImport_Click(object sender, EventArgs e)
        {
            var SPList = GetEntryList(1);
            var SPList_Dog = GetEntryList(2);
            using (OnlinePriem2012Entities priem = new OnlinePriem2012Entities())
            {
                foreach (var sp in SPList)
                {
                    priem.C_Entry.Add(sp);

                    try
                    {
                        priem.SaveChanges();
                    }
                    catch { }
                }
                foreach (var sp in SPList_Dog)
                {
                    priem.C_Entry.Add(sp);

                    try
                    {
                        priem.SaveChanges();
                    }
                    catch { }
                }

                MessageBox.Show("Done!");
            }
        }

        private List<C_Entry> GetEntryList(int StudyBasisId)
        {
            using (EducationEntities educCtx = new EducationEntities())
            {
                List<int?> ltsNeeded = new List<int?>() { 10, 8 };
                //нужны учебные планы для чётных семестров
                return educCtx.SP_StudyPlan.Where(x => x.SP_Status.IsEnabledForEntry
                    && ((x.SP_Year.IntValue + x.SP_EducationPeriod.YearIntVal) == /*(iNowYear + 1)*/2012)
                    && ltsNeeded.Contains(x.SP_ObrazProgram.SP_LicenseProgram.StudyLevelId)).ToList()
                    .Select(x =>
                        new C_Entry
                        {
                            Id = Guid.NewGuid(),
                            StudyPlanId = x.Id,
                            CampaignYear = iNowYear,
                            SemesterId = GetSemesterIdFromYear(x.SP_Year.IntValue ?? 1),
                            DateOfStart = new DateTime(2013, 12, 15),
                            DateOfClose = new DateTime(2014, 1, 15),
                            FacultyId = x.SP_ObrazProgram.FacultyId,
                            FacultyName = x.SP_ObrazProgram.SP_Faculty.Name,
                            IsDistance = x.IsDistance,
                            IsElectronic = x.IsElectronic,
                            IsExpress = x.IsExpress,
                            IsParallel = x.IsParallel,
                            IsReduced = x.IsReduced,
                            IsSecond = false,
                            LicenseProgramCode = x.SP_ObrazProgram.SP_LicenseProgram.Code,
                            LicenseProgramId = x.SP_ObrazProgram.LicenseProgramId.Value,
                            LicenseProgramName = x.SP_ObrazProgram.SP_LicenseProgram.Name,
                            LicenseProgramNameEng = x.SP_ObrazProgram.SP_LicenseProgram.NameEng,
                            ObrazProgramId = x.ObrazProgramId.Value,
                            ObrazProgramName = x.SP_ObrazProgram.Name,
                            ObrazProgramNameEng = x.SP_ObrazProgram.NameEng,
                            ProfileId = null,
                            ProfileName = "нет",
                            ProgramModeId = x.SP_ObrazProgram.ProgramModeId,
                            QualificationId = x.SP_ObrazProgram.SP_LicenseProgram.QualificationId,
                            QualificationName = x.SP_ObrazProgram.SP_LicenseProgram.QualificationId.HasValue ?
                                x.SP_ObrazProgram.SP_LicenseProgram.SP_Qualification.Name : null,
                            StudyBasisId = StudyBasisId,
                            StudyFormId = x.StudyFormId.Value,
                            StudyFormName = x.SP_StudyForm.Name,
                            StudyFormNameEng = x.SP_StudyForm.NameEng,
                            StudyLevelId = x.SP_ObrazProgram.SP_LicenseProgram.StudyLevelId ?? 1,
                            StudyLevelName = x.SP_ObrazProgram.SP_LicenseProgram.SP_StudyLevel.Name,
                            IsUsedForPriem = true
                        }).ToList();
            }
        }

        private int GetSemesterIdFromYear(int year)
        {
            //считаем года от начала
            int iYears = DateTime.Now.Year - year;
            int SemesterVal = iYears * 2 + (isOddSemester ? 2 : 1);
            return SemesterVal;
        }

        private void btnAddProfiles_Click(object sender, EventArgs e)
        {
            using (EducationEntities educCtx = new EducationEntities())
            using (OnlinePriem2012Entities priemCtx = new OnlinePriem2012Entities())
            {
                var entryList = priemCtx.C_Entry.Where(x => x.CampaignYear == iNowYear && x.SemesterId > 1 && x.DateOfClose > DateTime.Now).ToList();
                foreach (var ent in entryList)
                {
                    var sp_id = ent.StudyPlanId;
                    var sp_profiles_in_online = priemCtx.C_Entry.Where(x => x.StudyPlanId == sp_id).Select(x => new { x.ProfileId, x.ProfileName, x.ProfileNameEng }).ToList();
                    var sp_profiles_in_educdb = educCtx.SP_ProfileInStudyPlan.Where(x => x.StudyPlanId == sp_id)
                        .Select(x => new { ProfileId = (Guid?)x.Id, ProfileName = x.SP_Profile.Name, ProfileNameEng = x.SP_Profile.NameEng }).ToList();
                    var to_insert = sp_profiles_in_educdb.Except(sp_profiles_in_online).ToList();

                    foreach (var ins in to_insert)
                    {
                        var newEnt = new C_Entry();
                        newEnt.Id = Guid.NewGuid();
                        newEnt.DateOfClose = ent.DateOfClose;
                        newEnt.DateOfStart = ent.DateOfStart;
                        newEnt.AggregateGroupId = ent.AggregateGroupId;
                        newEnt.AggregateGroupName = ent.AggregateGroupName;
                        newEnt.CampaignYear = ent.CampaignYear;
                        newEnt.EducationPeriodId = ent.EducationPeriodId;
                        newEnt.EducationPeriodName = ent.EducationPeriodName;
                        newEnt.FacultyId = ent.FacultyId;
                        newEnt.FacultyName = ent.FacultyName;
                        newEnt.IsDistance = ent.IsDistance;
                        newEnt.IsElectronic = ent.IsElectronic;
                        newEnt.IsExpress = ent.IsExpress;
                        newEnt.IsParallel = ent.IsParallel;
                        newEnt.IsReduced = ent.IsReduced;
                        newEnt.IsSecond = ent.IsSecond;
                        newEnt.IsUsedForPriem = ent.IsUsedForPriem;
                        newEnt.LicenseProgramCode = ent.LicenseProgramCode;
                        newEnt.LicenseProgramId = ent.LicenseProgramId;
                        newEnt.LicenseProgramName = ent.LicenseProgramName;
                        newEnt.LicenseProgramNameEng = ent.LicenseProgramNameEng;
                        newEnt.ObrazProgramCrypt = ent.ObrazProgramCrypt;
                        newEnt.ObrazProgramId = ent.ObrazProgramId;
                        newEnt.ObrazProgramName = ent.ObrazProgramName;
                        newEnt.ObrazProgramNameEng = ent.ObrazProgramNameEng;
                        newEnt.ProgramModeId = ent.ProgramModeId;
                        newEnt.ProgramModeName = ent.ProgramModeName;
                        newEnt.QualificationId = ent.QualificationId;
                        newEnt.QualificationName = ent.QualificationName;
                        newEnt.SemesterId = ent.SemesterId;
                        newEnt.SeparateEntryProfileInSPId = ent.SeparateEntryProfileInSPId;
                        newEnt.StudyBasisAcronym = ent.StudyBasisAcronym;
                        newEnt.StudyBasisId = ent.StudyBasisId;
                        newEnt.StudyBasisName = ent.StudyBasisName;
                        newEnt.StudyBasisNameEng = ent.StudyBasisNameEng;
                        newEnt.StudyFormId = ent.StudyFormId;
                        newEnt.StudyFormName = ent.StudyFormName;
                        newEnt.StudyFormNameEng = ent.StudyFormNameEng;
                        newEnt.StudyLevelId = ent.StudyLevelId;
                        newEnt.StudyLevelName = ent.StudyLevelName;
                        newEnt.StudyPlanId = ent.StudyPlanId;
                        newEnt.StudyPlanNumber = ent.StudyPlanNumber;
                        newEnt.ProfileId = ins.ProfileId;
                        newEnt.ProfileName = ins.ProfileName;
                        newEnt.ProfileNameEng = ins.ProfileNameEng;

                        priemCtx.C_Entry.Add(newEnt);
                    }
                    priemCtx.SaveChanges();
                }
            }
        }
    }
}
