﻿// <auto-generated />
using System;
using MicroserviceBasedFintechApp.Identity.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceBasedFintechApp.Identity.Persistence.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20240620133026_TimestampWithTimeZone")]
    partial class TimestampWithTimeZone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ApiKey")
                        .HasColumnType("uuid")
                        .HasColumnName("api_key");

                    b.Property<DateTime>("CreationDateAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("creation_date_at_utc");

                    b.Property<string>("HashedSecret")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hashed_secret");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdateDateAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("update_date_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_companies");

                    b.HasIndex("ApiKey", "HashedSecret")
                        .IsUnique()
                        .HasDatabaseName("ix_companies_api_key_hashed_secret");

                    b.ToTable("companies", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
