//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriemForeignInspector.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class SP_Profile
    {
        public SP_Profile()
        {
            this.C_Entry = new HashSet<C_Entry>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Acronym { get; set; }
        public string AcronymEng { get; set; }
        public bool IsOpen { get; set; }
        public string Holder { get; set; }
    
        public virtual ICollection<C_Entry> C_Entry { get; set; }
    }
}
