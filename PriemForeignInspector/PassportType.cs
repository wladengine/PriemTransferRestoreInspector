//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriemForeignInspector
{
    using System;
    using System.Collections.Generic;
    
    public partial class PassportType
    {
        public PassportType()
        {
            this.Person = new HashSet<Person>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public Nullable<bool> IsApprovedForeign { get; set; }
    
        public virtual ICollection<Person> Person { get; set; }
    }
}
