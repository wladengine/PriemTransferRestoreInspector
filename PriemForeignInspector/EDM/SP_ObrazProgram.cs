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
    
    public partial class SP_ObrazProgram
    {
        public SP_ObrazProgram()
        {
            this.C_Entry = new HashSet<C_Entry>();
            this.PersonCurrentEducation = new HashSet<PersonCurrentEducation>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Number { get; set; }
        public int LicenseProgramId { get; set; }
        public int FacultyId { get; set; }
        public int ProgramModeId { get; set; }
        public bool IsExpress { get; set; }
        public bool IsOpen { get; set; }
        public string Holder { get; set; }
    
        public virtual ICollection<C_Entry> C_Entry { get; set; }
        public virtual ICollection<PersonCurrentEducation> PersonCurrentEducation { get; set; }
        public virtual SP_LicenseProgram SP_LicenseProgram { get; set; }
    }
}
