using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentInitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactional_out_box");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactional_out_box",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    api_key = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    idempotency_key = table.Column<Guid>(type: "uuid", nullable: false),
                    update_date_at_utc = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactional_out_box", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_transactional_out_box_api_key_idempotency_key",
                table: "transactional_out_box",
                columns: new[] { "api_key", "idempotency_key" },
                unique: true);
        }
    }
}
