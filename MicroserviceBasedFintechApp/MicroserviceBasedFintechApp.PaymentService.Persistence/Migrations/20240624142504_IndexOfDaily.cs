using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IndexOfDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date_at_utc",
                table: "aggregated_daily_orders",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date_at_utc",
                table: "aggregated_daily_orders",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "api_key",
                table: "aggregated_daily_orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_aggregated_daily_orders_api_key_date_aggregation_utc",
                table: "aggregated_daily_orders",
                columns: new[] { "api_key", "date_aggregation_utc" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_aggregated_daily_orders_api_key_date_aggregation_utc",
                table: "aggregated_daily_orders");

            migrationBuilder.DropColumn(
                name: "api_key",
                table: "aggregated_daily_orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "update_date_at_utc",
                table: "aggregated_daily_orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "creation_date_at_utc",
                table: "aggregated_daily_orders",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");
        }
    }
}
