﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AREA> AREAs { get; set; }
        public virtual DbSet<CITY> CITies { get; set; }
        public virtual DbSet<DISTRICT> DISTRICTs { get; set; }
        public virtual DbSet<FACILITY> FACILITies { get; set; }
        public virtual DbSet<FACILITY_PICTURES> FACILITY_PICTURES { get; set; }
        public virtual DbSet<FACILITY_TYPE> FACILITY_TYPE { get; set; }
        public virtual DbSet<SLIDER> SLIDERs { get; set; }
        public virtual DbSet<WAITED_USERS> WAITED_USERS { get; set; }
        public virtual DbSet<ANNOUNCEMENT> ANNOUNCEMENTS { get; set; }
        public virtual DbSet<RESERVATION> RESERVATIONs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<SUBSCRIBER> SUBSCRIBERs { get; set; }
    }
}