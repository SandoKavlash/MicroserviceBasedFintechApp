﻿// <auto-generated />
using System;
using MicroserviceBasedFintechApp.OrderService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceBasedFintechApp.OrderService.Persistence.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20240623090538_OrdersTable")]
    partial class OrdersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<Guid>("ApiKey")
                        .HasColumnType("uuid")
                        .HasColumnName("api_key");

                    b.Property<bool?>("Authenticated")
                        .HasColumnType("boolean")
                        .HasColumnName("authenticated");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<DateTime>("CreationDateAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("creation_date_at_utc");

                    b.Property<int>("Currency")
                        .HasColumnType("integer")
                        .HasColumnName("currency");

                    b.Property<Guid>("IdempotencyKey")
                        .HasColumnType("uuid")
                        .HasColumnName("idempotency_key");

                    b.Property<string>("SecretHashed")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("secret_hashed");

                    b.Property<int?>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdateDateAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("update_date_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("ApiKey", "IdempotencyKey")
                        .IsUnique()
                        .HasDatabaseName("ix_orders_api_key_idempotency_key");

                    b.ToTable("orders", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
