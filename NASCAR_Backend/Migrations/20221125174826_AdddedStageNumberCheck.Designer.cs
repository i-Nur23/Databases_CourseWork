﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NASCAR_Backend.Context;

#nullable disable

namespace NASCARBackend.Migrations
{
    [DbContext(typeof(NascarDbContext))]
    [Migration("20221125174826_AdddedStageNumberCheck")]
    partial class AdddedStageNumberCheck
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.2.22472.11")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NASCAR_Backend.Models.Change", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChangeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NewNumber")
                        .HasColumnType("int");

                    b.Property<int>("OldNumber")
                        .HasColumnType("int");

                    b.Property<int>("PilotID")
                        .HasColumnType("int");

                    b.Property<int>("StageNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PilotID");

                    b.HasIndex("StageNumber");

                    b.ToTable("CHANGE", t =>
                        {
                            t.HasCheckConstraint("NewNumber", "NewNumber >= 0 AND NewNumber <= 99");

                            t.HasCheckConstraint("OldNumber", "OldNumber >= 0 AND OldNumber <= 99");

                            t.HasCheckConstraint("StageNumber", "StageNumber >= 0 AND StageNumber <= 36");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ManufacturerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("BrandsCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Brand");

                    b.HasAlternateKey("Model");

                    b.ToTable("MANUFACTURER", t =>
                        {
                            t.HasCheckConstraint("Brand", "Brand IN ('Chevrolet', 'Ford', 'Toyota')");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Pilot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PilotID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("BirthCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("BirthState")
                        .HasColumnType("nvarchar(18)");

                    b.Property<int>("CarsNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("PerformanceStatus")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<bool>("PlayOffStatus")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Surname");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamID");

                    b.ToTable("PILOT", t =>
                        {
                            t.HasCheckConstraint("CarsNumber", "CarsNumber >= 0 AND CarsNumber <= 99");

                            t.HasCheckConstraint("PerformanceStatus", "PerformanceStatus IN ('OFF', 'ON', 'PT')");

                            t.HasCheckConstraint("Points", "Points >= 0");

                            t.HasCheckConstraint("Wins", "Wins >= 0 AND Wins <= 36");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Result", b =>
                {
                    b.Property<int>("PilotID")
                        .HasColumnType("int");

                    b.Property<int>("StageID")
                        .HasColumnType("int");

                    b.Property<int>("LeaderGap")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPitStops")
                        .HasColumnType("int");

                    b.Property<int>("Place")
                        .HasColumnType("int");

                    b.HasKey("PilotID", "StageID");

                    b.HasIndex("StageID");

                    b.ToTable("RESULT", t =>
                        {
                            t.HasCheckConstraint("LeaderGap", "LeaderGap >= 0");

                            t.HasCheckConstraint("NumberOfPitStops", "NumberOfPitStops >= 0");

                            t.HasCheckConstraint("Place", "Place >= 1");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Stage", b =>
                {
                    b.Property<int>("StageNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StageNumber"));

                    b.Property<DateTime>("EventsDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(60)")
                        .HasColumnName("StagesName");

                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.HasKey("StageNumber");

                    b.HasAlternateKey("EventsDate");

                    b.HasAlternateKey("Name");

                    b.HasIndex("TrackID");

                    b.ToTable("STAGE", t =>
                        {
                            t.HasCheckConstraint("StageNumber", "StageNumber >= 1 AND StageNumber <= 36")
                                .HasName("StageNumber1");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TeamID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FoundationYear")
                        .HasColumnType("int");

                    b.Property<string>("Founder")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(60)")
                        .HasColumnName("TeamsName");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("TEAM", t =>
                        {
                            t.HasCheckConstraint("FoundationYear", "FoundationYear >= 1900 AND FoundationYear <= 2021");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TrackID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(18)")
                        .HasColumnName("TracksCity");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(60)")
                        .HasColumnName("TracksName");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(18)")
                        .HasColumnName("TracksState");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("char(2)")
                        .HasColumnName("TracksType");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("TRACK", t =>
                        {
                            t.HasCheckConstraint("Length", "Length > 0");

                            t.HasCheckConstraint("TracksType", "TracksType IN ('SS', 'ST', 'RC')");
                        });
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Change", b =>
                {
                    b.HasOne("NASCAR_Backend.Models.Pilot", "Pilot")
                        .WithMany()
                        .HasForeignKey("PilotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NASCAR_Backend.Models.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pilot");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Pilot", b =>
                {
                    b.HasOne("NASCAR_Backend.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamID");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Result", b =>
                {
                    b.HasOne("NASCAR_Backend.Models.Pilot", "Pilot")
                        .WithMany()
                        .HasForeignKey("PilotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NASCAR_Backend.Models.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pilot");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Stage", b =>
                {
                    b.HasOne("NASCAR_Backend.Models.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("NASCAR_Backend.Models.Team", b =>
                {
                    b.HasOne("NASCAR_Backend.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });
#pragma warning restore 612, 618
        }
    }
}
