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
    
    public partial class PersonScienceWork
    {
        public System.Guid Id { get; set; }
        public System.Guid PersonId { get; set; }
        public int WorkTypeId { get; set; }
        public string WorkInfo { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
