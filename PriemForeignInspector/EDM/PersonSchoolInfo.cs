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
    
    public partial class PersonSchoolInfo
    {
        public System.Guid PersonId { get; set; }
        public int SchoolTypeId { get; set; }
        public int SchoolExitClassId { get; set; }
        public string SchoolAddress { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
