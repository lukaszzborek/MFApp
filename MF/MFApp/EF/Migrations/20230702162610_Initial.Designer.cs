﻿// <auto-generated />
using System;
using MFApp.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MFApp.EF.Migrations
{
    [DbContext(typeof(MFDbContext))]
    [Migration("20230702162610_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MFApp.Model.NipAccountNumber", b =>
                {
                    b.Property<string>("Nip")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Nip", "Number");

                    b.ToTable("NipAccountNumber");
                });

            modelBuilder.Entity("MFApp.Model.NipData", b =>
                {
                    b.Property<string>("Nip")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("HasVirtualAccounts")
                        .HasColumnType("bit");

                    b.Property<string>("Krs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("RegistrationDenialBasis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegistrationDenialDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RegistrationLegalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Regon")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("RemovalBasis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemovalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResidenceAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestorationBasis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RestorationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatusVat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nip");

                    b.ToTable("Nips");
                });

            modelBuilder.Entity("MFApp.Model.Person", b =>
                {
                    b.Property<string>("Nip")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nip", "Type");

                    b.ToTable("Person");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MFApp.Model.AuthorizedClerk", b =>
                {
                    b.HasBaseType("MFApp.Model.Person");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("MFApp.Model.Partner", b =>
                {
                    b.HasBaseType("MFApp.Model.Person");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("MFApp.Model.Representative", b =>
                {
                    b.HasBaseType("MFApp.Model.Person");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("MFApp.Model.NipAccountNumber", b =>
                {
                    b.HasOne("MFApp.Model.NipData", null)
                        .WithMany("AccountNumbers")
                        .HasForeignKey("Nip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MFApp.Model.AuthorizedClerk", b =>
                {
                    b.HasOne("MFApp.Model.NipData", null)
                        .WithMany("AuthorizedClerks")
                        .HasForeignKey("Nip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MFApp.Model.Partner", b =>
                {
                    b.HasOne("MFApp.Model.NipData", null)
                        .WithMany("Partners")
                        .HasForeignKey("Nip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MFApp.Model.Representative", b =>
                {
                    b.HasOne("MFApp.Model.NipData", null)
                        .WithMany("Representatives")
                        .HasForeignKey("Nip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MFApp.Model.NipData", b =>
                {
                    b.Navigation("AccountNumbers");

                    b.Navigation("AuthorizedClerks");

                    b.Navigation("Partners");

                    b.Navigation("Representatives");
                });
#pragma warning restore 612, 618
        }
    }
}
