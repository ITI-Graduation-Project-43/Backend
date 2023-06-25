using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class onCascadeDeleteForCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Chapters__Course__1CBC4616",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK__Chapters__Course__1CBC4616",
                table: "Chapters",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Chapters__Course__1CBC4616",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK__Chapters__Course__1CBC4616",
                table: "Chapters",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
