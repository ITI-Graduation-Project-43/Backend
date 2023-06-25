using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class updateStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumWishlist",
                table: "Students",
                newName: "NoOfWishlist");

            migrationBuilder.RenameColumn(
                name: "NumCourses",
                table: "Students",
                newName: "NoOfCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoOfWishlist",
                table: "Students",
                newName: "NumWishlist");

            migrationBuilder.RenameColumn(
                name: "NoOfCourses",
                table: "Students",
                newName: "NumCourses");
        }
    }
}
