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
    
    public partial class SP_LicenseProgram
    {
        public SP_LicenseProgram()
        {
            this.C_Entry = new HashSet<C_Entry>();
            this.PersonCurrentEducation = new HashSet<PersonCurrentEducation>();
            this.SP_ObrazProgram = new HashSet<SP_ObrazProgram>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Code { get; set; }
        public int StudyLevelId { get; set; }
        public int ProgramTypeId { get; set; }
        public string PositionNum { get; set; }
        public string NormativePeriod { get; set; }
        public Nullable<int> QualificationId { get; set; }
        public int AggregateGroupId { get; set; }
        public bool IsOpen { get; set; }
        public string Holder { get; set; }
        public string NewCode { get; set; }
    
        public virtual ICollection<C_Entry> C_Entry { get; set; }
        public virtual ICollection<PersonCurrentEducation> PersonCurrentEducation { get; set; }
        public virtual SP_StudyLevel SP_StudyLevel { get; set; }
        public virtual ICollection<SP_ObrazProgram> SP_ObrazProgram { get; set; }
    }
}
