// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;  
using Infrastructure.Data.Configuration;
using Core.Entities;

#nullable disable

namespace Infrastructure.Data
{
    public partial class myDbContext : DbContext
    {
        public myDbContext()
        {
        }

        public myDbContext(DbContextOptions<myDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelsConfiguration.Configuration(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}