﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlinePriem2012Entities : DbContext
    {
        public OnlinePriem2012Entities()
            : base("name=OnlinePriem2012Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<PriemApplication> Application { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<PersonAddInfo> PersonAddInfo { get; set; }
        public DbSet<PersonChangeStudyFormReason> PersonChangeStudyFormReason { get; set; }
        public DbSet<PersonContacts> PersonContacts { get; set; }
        public DbSet<PersonDisorderInfo> PersonDisorderInfo { get; set; }
        public DbSet<PersonSchoolInfo> PersonSchoolInfo { get; set; }
        public DbSet<PersonScienceWork> PersonScienceWork { get; set; }
        public DbSet<PersonSportQualification> PersonSportQualification { get; set; }
        public DbSet<PersonVisaInfo> PersonVisaInfo { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<SportQualification> SportQualification { get; set; }
        public DbSet<StudyBasis> StudyBasis { get; set; }
        public DbSet<StudyForm> StudyForm { get; set; }
        public DbSet<Semester> Semester { get; set; }
        public DbSet<PassportType> PassportType { get; set; }
        public DbSet<ApplicationCommit> ApplicationCommit { get; set; }
        public DbSet<ApplicationCommitVersion> ApplicationCommitVersion { get; set; }
        public DbSet<ApplicationCommitVersonDetails> ApplicationCommitVersonDetails { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Entry> Entry { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonHighEducationInfo> PersonHighEducationInfo { get; set; }
        public DbSet<PersonEducationDocument> PersonEducationDocument { get; set; }
        public DbSet<C_Entry> C_Entry { get; set; }
        public DbSet<PersonCurrentEducation> PersonCurrentEducation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<SP_Faculty_> SP_Faculty { get; set; }
        public DbSet<SP_LicenseProgram> SP_LicenseProgram { get; set; }
        public DbSet<SP_ObrazProgram> SP_ObrazProgram { get; set; }
        public DbSet<SP_Profile> SP_Profile { get; set; }
        public DbSet<SP_StudyLevel> SP_StudyLevel { get; set; }
        public DbSet<StudyLevelGroup> StudyLevelGroup { get; set; }
        public DbSet<AbiturientType> AbiturientType { get; set; }
        public DbSet<ApplicationSecondType> ApplicationSecondType { get; set; }
    }
}
