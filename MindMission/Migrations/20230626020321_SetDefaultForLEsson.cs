using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class SetDefaultForLEsson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NoOfHours",
                table: "Lessons",
                type: "decimal(3,2)",
                nullable: false,
                defaultValueSql: "0.5",
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFree",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NoOfHours",
                table: "Lessons",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldDefaultValueSql: "0.5");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFree",
                table: "Lessons",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");
        }
    }
}
