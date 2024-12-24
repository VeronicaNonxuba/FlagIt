﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FlaggingService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlaggingService.Data.Migrations
{
    [DbContext(typeof(FlaggingDbContext))]
    partial class FlaggingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "hstore");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlaggingService.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<Guid?>("EstablishmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("MobileNo")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Dictionary<string, string>>("SocialsInfo")
                        .HasColumnType("hstore");

                    b.Property<string>("TelephoneNo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EstablishmentId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("FlaggingService.Entities.Establishment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<List<Guid>>("ContactIds")
                        .HasColumnType("uuid[]");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FlagCount")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Owner")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TypeId");

                    b.ToTable("Establishments");
                });

            modelBuilder.Entity("FlaggingService.Entities.EstablishmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("EstablishmentType");
                });

            modelBuilder.Entity("FlaggingService.Entities.Flag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Significance")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("FlaggingService.Entities.Flagger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FlagCount")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlaggingService.Entities.Flagging", b =>
                {
                    b.Property<Guid>("FlagId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EstablishmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FlaggedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("FlaggedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("FlagId", "EstablishmentId", "FlaggedBy");

                    b.HasIndex("EstablishmentId");

                    b.HasIndex("FlaggedBy");

                    b.HasIndex("FlagId", "EstablishmentId", "FlaggedBy");

                    b.ToTable("Flagging");
                });

            modelBuilder.Entity("FlaggingService.Entities.Contact", b =>
                {
                    b.HasOne("FlaggingService.Entities.Establishment", "Establishment")
                        .WithMany("Contact")
                        .HasForeignKey("EstablishmentId");

                    b.Navigation("Establishment");
                });

            modelBuilder.Entity("FlaggingService.Entities.Establishment", b =>
                {
                    b.HasOne("FlaggingService.Entities.EstablishmentType", "EstType")
                        .WithMany("Establishments")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstType");
                });

            modelBuilder.Entity("FlaggingService.Entities.Flagging", b =>
                {
                    b.HasOne("FlaggingService.Entities.Establishment", "Establishment")
                        .WithMany("Flagging")
                        .HasForeignKey("EstablishmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlaggingService.Entities.Flag", "Flag")
                        .WithMany("Flagging")
                        .HasForeignKey("FlagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlaggingService.Entities.Flagger", "Flagger")
                        .WithMany("Flagging")
                        .HasForeignKey("FlaggedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Establishment");

                    b.Navigation("Flag");

                    b.Navigation("Flagger");
                });

            modelBuilder.Entity("FlaggingService.Entities.Establishment", b =>
                {
                    b.Navigation("Contact");

                    b.Navigation("Flagging");
                });

            modelBuilder.Entity("FlaggingService.Entities.EstablishmentType", b =>
                {
                    b.Navigation("Establishments");
                });

            modelBuilder.Entity("FlaggingService.Entities.Flag", b =>
                {
                    b.Navigation("Flagging");
                });

            modelBuilder.Entity("FlaggingService.Entities.Flagger", b =>
                {
                    b.Navigation("Flagging");
                });
#pragma warning restore 612, 618
        }
    }
}
