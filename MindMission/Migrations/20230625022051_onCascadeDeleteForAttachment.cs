using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class onCascadeDeleteForAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Attachmen__Lesso__3D2915A8",
                table: "Attachments");

            migrationBuilder.AddForeignKey(
                name: "FK__Attachmen__Lesso__3D2915A8",
                table: "Attachments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Attachmen__Lesso__3D2915A8",
                table: "Attachments");

            migrationBuilder.AddForeignKey(
                name: "FK__Attachmen__Lesso__3D2915A8",
                table: "Attachments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
