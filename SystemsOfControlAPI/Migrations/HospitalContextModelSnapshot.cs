﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemsOfControlAPI.Entities.Models;

#nullable disable

namespace SystemsOfControlAPI.Migrations
{
    [DbContext(typeof(HospitalContext))]
    partial class HospitalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Cabinet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Cabinets__3214EC276110F828");

                    b.ToTable("Cabinets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = 1
                        },
                        new
                        {
                            Id = 2,
                            Number = 2
                        },
                        new
                        {
                            Id = 3,
                            Number = 3
                        });
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__District__3214EC27AC5682AC");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = 0
                        },
                        new
                        {
                            Id = 2,
                            Number = 1
                        },
                        new
                        {
                            Id = 3,
                            Number = 2
                        });
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cabinet")
                        .HasColumnType("int");

                    b.Property<int?>("District")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Specialization")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Doctors__3214EC270A671B5F");

                    b.HasIndex("Cabinet");

                    b.HasIndex("District");

                    b.HasIndex("Specialization");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<int>("District")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Patients__3214EC27F38B4A7B");

                    b.HasIndex("District");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Speciali__3214EC27287A1B2D");

                    b.ToTable("Specializations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ophthalmologist"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Therapist"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Surgeon"
                        });
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Doctor", b =>
                {
                    b.HasOne("SystemsOfControlAPI.Entities.Models.Cabinet", "CabinetNavigation")
                        .WithMany("Doctors")
                        .HasForeignKey("Cabinet")
                        .IsRequired()
                        .HasConstraintName("FK__Doctors__Cabinet__403A8C7D");

                    b.HasOne("SystemsOfControlAPI.Entities.Models.District", "DistrictNavigation")
                        .WithMany("Doctors")
                        .HasForeignKey("District")
                        .HasConstraintName("FK__Doctors__Distric__4222D4EF");

                    b.HasOne("SystemsOfControlAPI.Entities.Models.Specialization", "SpecializationNavigation")
                        .WithMany("Doctors")
                        .HasForeignKey("Specialization")
                        .IsRequired()
                        .HasConstraintName("FK__Doctors__Special__412EB0B6");

                    b.Navigation("CabinetNavigation");

                    b.Navigation("DistrictNavigation");

                    b.Navigation("SpecializationNavigation");
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Patient", b =>
                {
                    b.HasOne("SystemsOfControlAPI.Entities.Models.District", "DistrictNavigation")
                        .WithMany("Patients")
                        .HasForeignKey("District")
                        .IsRequired()
                        .HasConstraintName("FK__Patients__Distri__3D5E1FD2");

                    b.Navigation("DistrictNavigation");
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Cabinet", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.District", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("SystemsOfControlAPI.Entities.Models.Specialization", b =>
                {
                    b.Navigation("Doctors");
                });
#pragma warning restore 612, 618
        }
    }
}
