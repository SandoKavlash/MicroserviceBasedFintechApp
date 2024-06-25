using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceBasedFintechApp.PaymentService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsPaidField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "payment_orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "payment_orders");
        }
    }
}
