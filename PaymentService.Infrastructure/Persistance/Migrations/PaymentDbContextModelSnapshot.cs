﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentService.Infrastructure.Persistance;

namespace PaymentService.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(PaymentDbContext))]
    partial class PaymentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("PaymentService.Domain.Entities.AccountingEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("EffectiveDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PolicyAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PolicyAccountId");

                    b.ToTable("AccountingEntry");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AccountingEntry");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.PolicyAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PolicyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("PolicyAccounts");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.ExpectedPayment", b =>
                {
                    b.HasBaseType("PaymentService.Domain.Entities.AccountingEntry");

                    b.HasDiscriminator().HasValue("ExpectedPayment");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.InPayment", b =>
                {
                    b.HasBaseType("PaymentService.Domain.Entities.AccountingEntry");

                    b.HasDiscriminator().HasValue("InPayment");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.OutPayment", b =>
                {
                    b.HasBaseType("PaymentService.Domain.Entities.AccountingEntry");

                    b.HasDiscriminator().HasValue("OutPayment");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.AccountingEntry", b =>
                {
                    b.HasOne("PaymentService.Domain.Entities.PolicyAccount", "PolicyAccount")
                        .WithMany("Entries")
                        .HasForeignKey("PolicyAccountId");
                });

            modelBuilder.Entity("PaymentService.Domain.Entities.PolicyAccount", b =>
                {
                    b.HasOne("PaymentService.Domain.Entities.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}