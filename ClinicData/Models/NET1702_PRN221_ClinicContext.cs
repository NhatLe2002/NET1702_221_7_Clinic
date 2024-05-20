using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClinicData.Models
{
    public partial class NET1702_PRN221_ClinicContext : DbContext
    {
        public NET1702_PRN221_ClinicContext()
        {
        }

        public NET1702_PRN221_ClinicContext(DbContextOptions<NET1702_PRN221_ClinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppoimentDetail> AppoimentDetails { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Clinic> Clinics { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Dentist> Dentists { get; set; } = null!;
        public virtual DbSet<ExaminationResult> ExaminationResults { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=12345;Database=NET1702_PRN221_Clinic;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppoimentDetail>(entity =>
            {
                entity.HasKey(e => e.AppoimnetDetailId)
                    .HasName("PK__Appoimen__90CC0291F5045DA4");

                entity.Property(e => e.AppoimnetDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("AppoimnetDetailID");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DentistId).HasColumnName("DentistID");

                entity.Property(e => e.ExaminationResultId).HasColumnName("ExaminationResultID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppoimentDetails)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__Appoiment__Appoi__3B75D760");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.AppoimentDetails)
                    .HasForeignKey(d => d.ClinicId)
                    .HasConstraintName("FK__Appoiment__Clini__38996AB5");

                entity.HasOne(d => d.Dentist)
                    .WithMany(p => p.AppoimentDetails)
                    .HasForeignKey(d => d.DentistId)
                    .HasConstraintName("FK__Appoiment__Denti__398D8EEE");

                entity.HasOne(d => d.ExaminationResult)
                    .WithMany(p => p.AppoimentDetails)
                    .HasForeignKey(d => d.ExaminationResultId)
                    .HasConstraintName("FK__Appoiment__Exami__3C69FB99");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.AppoimentDetails)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Appoiment__Servi__3A81B327");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("AppointmentID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Appointme__Custo__31EC6D26");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.Property(e => e.ClinicId)
                    .ValueGeneratedNever()
                    .HasColumnName("ClinicID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.ClinicName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Customers__UserI__2F10007B");
            });

            modelBuilder.Entity<Dentist>(entity =>
            {
                entity.Property(e => e.DentistId)
                    .ValueGeneratedNever()
                    .HasColumnName("DentistID");

                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DentistName).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.ProfessionalQualifications).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Dentists)
                    .HasForeignKey(d => d.ClinicId)
                    .HasConstraintName("FK__Dentists__Clinic__2C3393D0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Dentists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Dentists__UserID__2B3F6F97");
            });

            modelBuilder.Entity<ExaminationResult>(entity =>
            {
                entity.Property(e => e.ExaminationResultId)
                    .ValueGeneratedNever()
                    .HasColumnName("ExaminationResultID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ReExaminationDate).HasColumnType("date");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ServiceID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ServiceName).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleID__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
