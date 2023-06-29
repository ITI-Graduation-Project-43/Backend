using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class uniqeuCourseStudentFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseFeedbacks_StudentId",
                table: "CourseFeedbacks");

            migrationBuilder.RenameIndex(
                name: "idx_coursefeedbacks_courseid",
                table: "CourseFeedbacks",
                newName: "IX_CourseFeedbacks_CourseId");

            migrationBuilder.CreateIndex(
                name: "idx_coursefeedbacks_student_course",
                table: "CourseFeedbacks",
                columns: new[] { "StudentId", "CourseId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_coursefeedbacks_student_course",
                table: "CourseFeedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFeedbacks_CourseId",
                table: "CourseFeedbacks",
                newName: "idx_coursefeedbacks_courseid");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeedbacks_StudentId",
                table: "CourseFeedbacks",
                column: "StudentId");
        }
    }
}
