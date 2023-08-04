using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SystemsOfControlAPI.Entities.Models;

public partial class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433\\MSSQL;Database=Hospital;Trusted_Connection=False;TrustServerCertificate=True;User=sa;Password=yourStrong(!)Password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PK__Cabinets__78A1A19CFB1A0891");

            entity.Property(e => e.Number).ValueGeneratedNever();
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Number).HasName("PK__District__78A1A19C3DDF1D75");

            entity.ToTable("District");

            entity.Property(e => e.Number).ValueGeneratedNever();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctors__3214EC273D471EE0");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Specialization).HasMaxLength(100);

            entity.HasOne(d => d.CabinetNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.Cabinet)
                .HasConstraintName("FK__Doctors__Cabinet__4CA06362");

            entity.HasOne(d => d.DistrictNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.District)
                .HasConstraintName("FK__Doctors__Distric__4E88ABD4");

            entity.HasOne(d => d.SpecializationNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.Specialization)
                .HasConstraintName("FK__Doctors__Special__4D94879B");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patients__3214EC27A9D32C4C");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Adress).HasMaxLength(200);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Sex).HasMaxLength(1);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.DistrictNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.District)
                .HasConstraintName("FK__Patients__Distri__49C3F6B7");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__Speciali__737584F7D7AD2E06");

            entity.ToTable("Specialization");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
