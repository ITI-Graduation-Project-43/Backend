using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class modifyAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountType",
                table: "Accounts",
                newName: "AccountDomain");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Accounts",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "AccountDomain",
                table: "Accounts",
                newName: "AccountType");
        }
    }
}
