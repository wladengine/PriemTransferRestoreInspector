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
    
    public partial class Qualification
    {
        public Qualification()
        {
            this.PersonHighEducationInfo = new HashSet<PersonHighEducationInfo>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public bool IsForAspirant { get; set; }
    
        public virtual ICollection<PersonHighEducationInfo> PersonHighEducationInfo { get; set; }
    }
}
