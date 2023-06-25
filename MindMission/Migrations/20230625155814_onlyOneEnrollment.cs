using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class onlyOneEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CourseId_StudentId",
                table: "Wishlists",
                columns: new[] { "CourseId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId_StudentId",
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wishlists_CourseId_StudentId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseId_StudentId",
                table: "Enrollments");
        }
    }
}
