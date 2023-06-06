using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindMission.API.Migrations
{
    public partial class updatecoursetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WhatWillLearn",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "WholsFor",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Courses",
                type: "varchar(2048)",
                unicode: false,
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhatWillLearn",
                table: "Courses",
                type: "varchar(2048)",
                unicode: false,
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WholsFor",
                table: "Courses",
                type: "varchar(2048)",
                unicode: false,
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }
    }
}
