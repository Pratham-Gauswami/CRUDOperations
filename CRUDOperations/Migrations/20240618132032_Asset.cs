using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDOperations.Migrations
{
    /// <inheritdoc />
    public partial class Asset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_logins",
                table: "logins");

            migrationBuilder.RenameTable(
                name: "logins",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Asset_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Employee_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asset_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Make_Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Date_of_assign = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_of_req = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Asset_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "logins");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "logins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_logins",
                table: "logins",
                column: "Username");
        }
    }
}
