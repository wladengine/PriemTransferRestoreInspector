//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriemForeignInspector
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriemApplication
    {
        public System.Guid Id { get; set; }
        public System.Guid PersonId { get; set; }
        public int Priority { get; set; }
        public int Barcode { get; set; }
        public bool Enabled { get; set; }
        public int EntryType { get; set; }
        public bool HostelEduc { get; set; }
        public Nullable<System.DateTime> DateOfDisable { get; set; }
        public System.Guid EntryId { get; set; }
        public System.DateTime DateOfStart { get; set; }
        public bool IsApprovedByComission { get; set; }
        public bool IsImported { get; set; }
        public Nullable<int> SecondTypeId { get; set; }
        public Nullable<System.DateTime> DateReviewDocs { get; set; }
        public System.Guid CommitId { get; set; }
        public bool IsCommited { get; set; }
        public bool IsGosLine { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> CompetitionId { get; set; }
        public string ApproverName { get; set; }
        public Nullable<System.DateTime> DocInsertDate { get; set; }
    
        public virtual C_Entry C_Entry { get; set; }
    }
}
