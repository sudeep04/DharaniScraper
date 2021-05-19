using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DharaniScaper.Models
{
    public partial class DharaniXContext : DbContext
    {
        public DharaniXContext()
        {
        }

        public DharaniXContext(DbContextOptions<DharaniXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Khatum> Khata { get; set; }
        public virtual DbSet<Mandal> Mandals { get; set; }
        public virtual DbSet<ResultsInfo> ResultsInfos { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyInfo> SurveyInfos { get; set; }
        public virtual DbSet<Village> Villages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DharaniX;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Khatum>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Mandal>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ResultsInfo>(entity =>
            {
                entity.ToTable("ResultsInfo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ppbnumber).HasColumnName("PPBNumber");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SurveyInfo>(entity =>
            {
                entity.ToTable("SurveyInfo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Ppbnumber).HasColumnName("PPBNumber");
            });

            modelBuilder.Entity<Village>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
