﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EducationEntities : DbContext
    {
        public EducationEntities()
            : base("name=EducationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<SP_EducationDocument> SP_EducationDocument { get; set; }
        public DbSet<SP_EducationPeriod> SP_EducationPeriod { get; set; }
        public DbSet<SP_EducationPermit> SP_EducationPermit { get; set; }
        public DbSet<SP_Faculty> SP_Faculty { get; set; }
        public DbSet<SP_Language> SP_Language { get; set; }
        public DbSet<SP_LicenseProgram> SP_LicenseProgram { get; set; }
        public DbSet<SP_ObrazProgram> SP_ObrazProgram { get; set; }
        public DbSet<SP_ObrazProgramInYear> SP_ObrazProgramInYear { get; set; }
        public DbSet<SP_PayPeriod> SP_PayPeriod { get; set; }
        public DbSet<SP_Profile> SP_Profile { get; set; }
        public DbSet<SP_ProfileInStudyPlan> SP_ProfileInStudyPlan { get; set; }
        public DbSet<SP_Qualification> SP_Qualification { get; set; }
        public DbSet<SP_StudyBasis> SP_StudyBasis { get; set; }
        public DbSet<SP_StudyForm> SP_StudyForm { get; set; }
        public DbSet<SP_StudyLevel> SP_StudyLevel { get; set; }
        public DbSet<SP_StudyLevelGroup> SP_StudyLevelGroup { get; set; }
        public DbSet<SP_StudyPlan> SP_StudyPlan { get; set; }
        public DbSet<extStudyPlan> extStudyPlan { get; set; }
        public DbSet<SP_Status> SP_Status { get; set; }
        public DbSet<SP_Year> SP_Year { get; set; }
    }
}
