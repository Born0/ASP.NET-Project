﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PcHut.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class pchutEntities2 : DbContext
    {
        public pchutEntities2()
            : base("name=pchutEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<brand> brands { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<credential> credentials { get; set; }
        public virtual DbSet<invoice> invoices { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<sales_record> sales_record { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<vendor> vendors { get; set; }
    }
}
