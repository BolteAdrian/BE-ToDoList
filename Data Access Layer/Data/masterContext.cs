using System;
using System.Collections.Generic;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data_Access_Layer.Data
{
    public partial class masterContext : DbContext
    {
        public masterContext()
        {
        }

        public masterContext(DbContextOptions<masterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PrincipalSecondary> PrincipalSecondaries { get; set; } = null!;
        public virtual DbSet<PrincipalTask> PrincipalTasks { get; set; } = null!;
        public virtual DbSet<SecondaryTask> SecondaryTasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-SQI6EKP;Database=master;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrincipalSecondary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("principal_secondary");

                entity.Property(e => e.AssignedUser)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Assigned_user");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_date");

                entity.Property(e => e.Objective)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StAssignedUser)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("st_assigned_user");

                entity.Property(e => e.StChecked).HasColumnName("st_checked");

                entity.Property(e => e.StDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("st_description");

                entity.Property(e => e.StEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("st_end_date");

                entity.Property(e => e.StObjective)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("st_objective");

                entity.Property(e => e.StPriority).HasColumnName("st_priority");

                entity.Property(e => e.StStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("st_start_date");

                entity.Property(e => e.StTitle)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("st_title");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PrincipalTask>(entity =>
            {
                entity.ToTable("Principal_task");

                entity.Property(e => e.AssignedUser)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Assigned_user");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_date");

                entity.Property(e => e.Objective)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SecondaryTask>(entity =>
            {
                entity.ToTable("Secondary_task");

                entity.Property(e => e.AssignedUser)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Assigned_user");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_date");

                entity.Property(e => e.Objective)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PrincipalTaskId).HasColumnName("Principal_task_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_date");

                entity.Property(e => e.Title)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.PrincipalTask)
                    .WithMany(p => p.SecondaryTasks)
                    .HasForeignKey(d => d.PrincipalTaskId)
                    .HasConstraintName("FK__Secondary__Princ__278EDA44");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
