﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineSchool.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OSMSEntities : DbContext
    {
        public OSMSEntities()
            : base("name=OSMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<ATTENDANCE> ATTENDANCEs { get; set; }
        public virtual DbSet<CLASS> CLASSes { get; set; }
        public virtual DbSet<CLASSROUTINE> CLASSROUTINEs { get; set; }
        public virtual DbSet<EVENT> EVENTs { get; set; }
        public virtual DbSet<GUARDIAN> GUARDIANs { get; set; }
        public virtual DbSet<GUARDIANREVIEW> GUARDIANREVIEWs { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<RESULT> RESULTs { get; set; }
        public virtual DbSet<SECTION> SECTIONs { get; set; }
        public virtual DbSet<STUDENT> STUDENTs { get; set; }
        public virtual DbSet<SUBJECT> SUBJECTs { get; set; }
        public virtual DbSet<TEACHER> TEACHERs { get; set; }
        public virtual DbSet<TEACHER_ROUTINE> TEACHER_ROUTINE { get; set; }
    }
}
