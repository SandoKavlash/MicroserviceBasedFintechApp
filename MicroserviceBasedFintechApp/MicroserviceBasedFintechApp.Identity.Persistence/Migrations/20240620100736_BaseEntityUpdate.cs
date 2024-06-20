using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceBasedFintechApp.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "row_create_date",
                table: "companies",
                newName: "update_date_at_utc");

            migrationBuilder.RenameColumn(
                name: "company_name",
                table: "companies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "companies",
                newName: "id");

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_date_at_utc",
                table: "companies",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creation_date_at_utc",
                table: "companies");

            migrationBuilder.RenameColumn(
                name: "update_date_at_utc",
                table: "companies",
                newName: "row_create_date");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "companies",
                newName: "company_name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "companies",
                newName: "company_id");
        }
    }
}
