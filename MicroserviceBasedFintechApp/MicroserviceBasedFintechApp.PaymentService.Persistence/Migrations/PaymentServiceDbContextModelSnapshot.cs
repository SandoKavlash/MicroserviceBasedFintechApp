﻿// <auto-generated />
using System;
using MicroserviceBasedFintechApp.PaymentService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    [DbContext(typeof(PaymentServiceDbContext))]
    partial class PaymentServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities.AggregatedOrdersDaily", b =>
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

                    b.Property<DateTime>("CreationDateAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("creation_date_at_utc");

                    b.Property<DateTime>("DateAggregationUTC")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_aggregation_utc");

                    b.Property<DateTime>("UpdateDateAtUtc")
                        .HasColumnType("timestamptz")
                        .HasColumnName("update_date_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_aggregated_daily_orders");

                    b.HasIndex("ApiKey", "DateAggregationUTC")
                        .IsUnique()
                        .HasDatabaseName("ix_aggregated_daily_orders_api_key_date_aggregation_utc");

                    b.ToTable("aggregated_daily_orders", (string)null);
                });

            modelBuilder.Entity("MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities.PaymentOrder", b =>
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

                    b.Property<bool>("IsPaid")
                        .HasColumnType("boolean")
                        .HasColumnName("is_paid");

                    b.Property<bool>("OrderServiceNotifier")
                        .HasColumnType("boolean")
                        .HasColumnName("order_service_notifier");

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
                        .HasName("pk_payment_orders");

                    b.HasIndex("ApiKey", "IdempotencyKey")
                        .IsUnique()
                        .HasDatabaseName("ix_payment_orders_api_key_idempotency_key");

                    b.ToTable("payment_orders", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
