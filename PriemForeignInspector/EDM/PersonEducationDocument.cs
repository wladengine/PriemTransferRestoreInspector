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
    
    public partial class PersonEducationDocument
    {
        public int Id { get; set; }
        public System.Guid PersonId { get; set; }
        public int SchoolTypeId { get; set; }
        public int CountryEducId { get; set; }
        public int RegionEducId { get; set; }
        public Nullable<int> VuzAdditionalTypeId { get; set; }
        public string SchoolCity { get; set; }
        public string SchoolName { get; set; }
        public string SchoolNum { get; set; }
        public string SchoolExitYear { get; set; }
        public Nullable<int> SchoolExitClassId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public bool IsEqual { get; set; }
        public string EqualDocumentNumber { get; set; }
        public Nullable<double> AvgMark { get; set; }
        public bool IsExcellent { get; set; }
    
        public virtual Country CountryEduc { get; set; }
        public virtual Person Person { get; set; }
        public virtual Region RegionEduc { get; set; }
        public virtual PersonHighEducationInfo PersonHighEducationInfo { get; set; }
    }
}
