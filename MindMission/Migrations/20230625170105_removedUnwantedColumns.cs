using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class removedUnwantedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfQuestions",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ChapterCount",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LessonCount",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NoOfArticles",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NoOfAttachments",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NoOfHours",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NoOfQuizzes",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NoOfVideos",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NoOfHours",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "NoOfLessons",
                table: "Chapters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfQuestions",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChapterCount",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonCount",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfArticles",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfAttachments",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "NoOfHours",
                table: "Courses",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NoOfQuizzes",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfVideos",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "NoOfHours",
                table: "Chapters",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NoOfLessons",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
