//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriemForeignInspector.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entry
    {
        public System.Guid Id { get; set; }
        public int SemesterId { get; set; }
        public Nullable<System.Guid> StudyPlanId { get; set; }
        public string StudyPlanNumber { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public int LicenseProgramId { get; set; }
        public int ObrazProgramId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileNameEng { get; set; }
        public int StudyBasisId { get; set; }
        public int StudyFormId { get; set; }
        public int StudyLevelId { get; set; }
        public int ProfileId { get; set; }
        public bool IsSecond { get; set; }
        public bool IsReduced { get; set; }
        public bool IsParallel { get; set; }
        public bool IsExpress { get; set; }
        public bool IsElectronic { get; set; }
        public bool IsDistance { get; set; }
        public int CampaignYear { get; set; }
        public Nullable<System.DateTime> DateOfClose { get; set; }
        public Nullable<System.DateTime> DateOfStart { get; set; }
        public Nullable<System.DateTime> DateOfClose_Foreign { get; set; }
        public Nullable<System.DateTime> DateOfStart_Foreign { get; set; }
        public Nullable<System.DateTime> DateOfClose_GosLine { get; set; }
        public Nullable<System.DateTime> DateOfStart_GosLine { get; set; }
        public bool IsUsedForPriem { get; set; }
        public string ObrazProgramName { get; set; }
        public string ObrazProgramNameEng { get; set; }
        public string Number { get; set; }
        public string StudyLevelName { get; set; }
        public string FacultyName { get; set; }
        public string LicenseProgramName { get; set; }
        public string LicenseProgramNameEng { get; set; }
        public string LicenseProgramCode { get; set; }
        public string StudyFormName { get; set; }
        public string StudyBasisName { get; set; }
        public string ObrazProgramCrypt { get; set; }
        public string StudyBasisNameEng { get; set; }
        public string StudyFormNameEng { get; set; }
        public int StudyLevelGroupId { get; set; }
        public string StudyLevelGroupNameEng { get; set; }
        public string StudyLevelGroupNameRus { get; set; }
        public string StudyLevelNameEng { get; set; }
        public Nullable<int> ComissionId { get; set; }
        public string Address { get; set; }
        public string LicenseProgramNewCode { get; set; }
        public bool IsForeign { get; set; }
        public bool IsCrimea { get; set; }
    }
}
