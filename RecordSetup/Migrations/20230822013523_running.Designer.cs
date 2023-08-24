﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordSetup.Context;

#nullable disable

namespace RecordSetup.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230822013523_running")]
    partial class running
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RecordSetup.Entities.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("RecordSetup.Entities.SubRegionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("SubRegionRecords");
                });

            modelBuilder.Entity("RecordSetup.Entities.SubRegionRecordTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SubRegionRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubRegionRecordId");

                    b.ToTable("SubRegionRecordTables");
                });

            modelBuilder.Entity("RecordSetup.Entities.TableSchema", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SubRegionRecordTableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubRegionRecordTableId");

                    b.ToTable("TableSchemas");
                });

            modelBuilder.Entity("RecordSetup.Entities.SubRegionRecord", b =>
                {
                    b.HasOne("RecordSetup.Entities.Region", "Region")
                        .WithMany("SubRegions")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("RecordSetup.Entities.SubRegionRecordTable", b =>
                {
                    b.HasOne("RecordSetup.Entities.SubRegionRecord", "SubRegionRecord")
                        .WithMany("SubRegionRecordTables")
                        .HasForeignKey("SubRegionRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubRegionRecord");
                });

            modelBuilder.Entity("RecordSetup.Entities.TableSchema", b =>
                {
                    b.HasOne("RecordSetup.Entities.SubRegionRecordTable", "SubRegionRecordTable")
                        .WithMany("TableSchemas")
                        .HasForeignKey("SubRegionRecordTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubRegionRecordTable");
                });

            modelBuilder.Entity("RecordSetup.Entities.Region", b =>
                {
                    b.Navigation("SubRegions");
                });

            modelBuilder.Entity("RecordSetup.Entities.SubRegionRecord", b =>
                {
                    b.Navigation("SubRegionRecordTables");
                });

            modelBuilder.Entity("RecordSetup.Entities.SubRegionRecordTable", b =>
                {
                    b.Navigation("TableSchemas");
                });
#pragma warning restore 612, 618
        }
    }
}
