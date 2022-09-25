using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Lesson1.Entities;

namespace Lesson1.Data
{
    public partial class WorksTokContext : DbContext
    {
        public WorksTokContext()
        {
        }

        public WorksTokContext(DbContextOptions<WorksTokContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Workrequest> Workrequests { get; set; } = null!;
        public virtual DbSet<Worktype> Worktypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=0000;database=workstokv1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth).HasMaxLength(20);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.HasIndex(e => e.EmployeeId, "fk_Schedule_Employee1_idx");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FinishWorkTime).HasColumnType("time");

                entity.Property(e => e.StartWorkTime).HasColumnType("time");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Schedule_Employee1");
            });

            modelBuilder.Entity<Workrequest>(entity =>
            {
                entity.ToTable("workrequest");

                entity.HasIndex(e => e.ClientId, "fk_BuildingRequest_Client1_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_BuildingRequest_Employee1_idx");

                entity.HasIndex(e => e.WorkTypeId, "fk_BuildingRequest_WorkType1_idx");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BeginWorkTime).HasColumnType("datetime");

                entity.Property(e => e.CompleteWorkTime).HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FullfilledDate).HasColumnType("datetime");

                entity.Property(e => e.IsAccepted).HasColumnType("bit(1)");

                entity.Property(e => e.TaskDescription).HasMaxLength(300);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Workrequests)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BuildingRequest_Client1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Workrequests)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BuildingRequest_Employee1");

                entity.HasOne(d => d.WorkType)
                    .WithMany(p => p.Workrequests)
                    .HasForeignKey(d => d.WorkTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_BuildingRequest_WorkType1");
            });

            modelBuilder.Entity<Worktype>(entity =>
            {
                entity.ToTable("worktype");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasMany(d => d.Employees)
                    .WithMany(p => p.WorkTypes)
                    .UsingEntity<Dictionary<string, object>>(
                        "Worktypeofemployee",
                        l => l.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_Employee_has_WorkType_Employee"),
                        r => r.HasOne<Worktype>().WithMany().HasForeignKey("WorkTypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_Employee_has_WorkType_WorkType1"),
                        j =>
                        {
                            j.HasKey("WorkTypeId", "EmployeeId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("worktypeofemployee");

                            j.HasIndex(new[] { "EmployeeId" }, "fk_Employee_has_WorkType_Employee_idx");

                            j.HasIndex(new[] { "WorkTypeId" }, "fk_Employee_has_WorkType_WorkType1_idx");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
