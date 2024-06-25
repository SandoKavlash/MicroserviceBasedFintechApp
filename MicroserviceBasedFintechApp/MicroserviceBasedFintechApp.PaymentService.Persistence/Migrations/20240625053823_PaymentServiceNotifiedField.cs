using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentServiceNotifiedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "order_service_notifier",
                table: "payment_orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_service_notifier",
                table: "payment_orders");
        }
    }
}
