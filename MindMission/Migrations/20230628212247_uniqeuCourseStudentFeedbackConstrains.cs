using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class uniqeuCourseStudentFeedbackConstrains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedbacks_StudentId_CourseId",
                table: "CourseFeedbacks",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseFeedbacks_StudentId_CourseId",
                table: "CourseFeedbacks");
        }
    }
}
