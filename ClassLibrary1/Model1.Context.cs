﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OQDevSNAPEntities : DbContext
    {
        public OQDevSNAPEntities()
            : base("name=OQDevSNAPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GroupActivityQuestion> GroupActivityQuestions { get; set; }
        public virtual DbSet<GroupActivityAnswer> GroupActivityAnswers { get; set; }
        public virtual DbSet<GroupActivityOption> GroupActivityOptions { get; set; }
        public virtual DbSet<GroupActivityTestScore> GroupActivityTestScores { get; set; }
        public virtual DbSet<GroupActivityText> GroupActivityTexts { get; set; }
        public virtual DbSet<GroupActivityType> GroupActivityTypes { get; set; }
        public virtual DbSet<GroupCompletedActivity> GroupCompletedActivities { get; set; }
        public virtual DbSet<GroupCompletedPackage> GroupCompletedPackages { get; set; }
        public virtual DbSet<GroupPackageActivity> GroupPackageActivities { get; set; }
        public virtual DbSet<GroupPackageName> GroupPackageNames { get; set; }
        public virtual DbSet<GroupProgram> GroupPrograms { get; set; }
    }
}