using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class onCascadeDeleteForLessonChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Articles__Lesson__395884C4",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK__Questions__QuizI__30C33EC3",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK__Quizzes__LessonI__2B0A656D",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK__Videos__LessonId__3587F3E0",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK__Articles__Lesson__395884C4",
                table: "Articles",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Questions__QuizI__30C33EC3",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Quizzes__LessonI__2B0A656D",
                table: "Quizzes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Videos__LessonId__3587F3E0",
                table: "Videos",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Articles__Lesson__395884C4",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK__Questions__QuizI__30C33EC3",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK__Quizzes__LessonI__2B0A656D",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK__Videos__LessonId__3587F3E0",
                table: "Videos");

            migrationBuilder.AddForeignKey(
                name: "FK__Articles__Lesson__395884C4",
                table: "Articles",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Questions__QuizI__30C33EC3",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Quizzes__LessonI__2B0A656D",
                table: "Quizzes",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Videos__LessonId__3587F3E0",
                table: "Videos",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
