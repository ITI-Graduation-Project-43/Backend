using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class onCascadeDeleteForChapter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Lessons__Chapter__245D67DE",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK__Lessons__Chapter__245D67DE",
                table: "Lessons",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Lessons__Chapter__245D67DE",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK__Lessons__Chapter__245D67DE",
                table: "Lessons",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id");
        }
    }
}
