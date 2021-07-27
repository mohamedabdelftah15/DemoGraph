using System;
using DemoGraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoGraph.Data
{
    public partial class DemoGraphContext : DbContext
    {
        public DemoGraphContext()
        {
        }

        public DemoGraphContext(DbContextOptions<DemoGraphContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DemographicType> DemographicType { get; set; }
        public virtual DbSet<DemographicTypeDtl> DemographicTypeDtl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=DemoGraphDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemographicTypeDtl>(entity =>
            {
                entity.HasOne(d => d.DemoType)
                    .WithMany(p => p.DemographicTypeDtl)
                    .HasForeignKey(d => d.DemoTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DemograghicTypeDTL_DemographicType");
            });
        }
    }
}
