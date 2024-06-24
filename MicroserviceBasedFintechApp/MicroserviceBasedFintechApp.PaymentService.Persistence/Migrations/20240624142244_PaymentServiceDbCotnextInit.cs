using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentServiceDbCotnextInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aggregated_daily_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_aggregation_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    update_date_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creation_date_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aggregated_daily_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: true),
                    currency = table.Column<int>(type: "integer", nullable: false),
                    idempotency_key = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: true),
                    api_key = table.Column<Guid>(type: "uuid", nullable: false),
                    secret_hashed = table.Column<string>(type: "text", nullable: false),
                    authenticated = table.Column<bool>(type: "boolean", nullable: true),
                    update_date_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    creation_date_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_orders", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_payment_orders_api_key_secret_hashed",
                table: "payment_orders",
                columns: new[] { "api_key", "secret_hashed" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aggregated_daily_orders");

            migrationBuilder.DropTable(
                name: "payment_orders");
        }
    }
}
