using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class correct_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_payment_orders_api_key_secret_hashed",
                table: "payment_orders");

            migrationBuilder.CreateIndex(
                name: "ix_payment_orders_api_key_idempotency_key",
                table: "payment_orders",
                columns: new[] { "api_key", "idempotency_key" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_payment_orders_api_key_idempotency_key",
                table: "payment_orders");

            migrationBuilder.CreateIndex(
                name: "ix_payment_orders_api_key_secret_hashed",
                table: "payment_orders",
                columns: new[] { "api_key", "secret_hashed" },
                unique: true);
        }
    }
}
