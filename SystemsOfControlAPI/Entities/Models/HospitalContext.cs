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
            entity.HasKey(e => e.Id).HasName("PK__Cabinets__3214EC276110F828");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__District__3214EC27AC5682AC");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctors__3214EC270A671B5F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).HasMaxLength(100);

            entity.HasOne(d => d.CabinetNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.Cabinet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__Cabinet__403A8C7D");

            entity.HasOne(d => d.DistrictNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.District)
                .HasConstraintName("FK__Doctors__Distric__4222D4EF");

            entity.HasOne(d => d.SpecializationNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.Specialization)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__Special__412EB0B6");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patients__3214EC27F38B4A7B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Sex).HasMaxLength(10);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.DistrictNavigation).WithMany(p => p.Patients)
                .HasForeignKey(d => d.District)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patients__Distri__3D5E1FD2");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Speciali__3214EC27287A1B2D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
