using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademyManagement.Persistence.Migrations
{
    public partial class Add_Auditabel_Fields_To_Lesson_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedTime",
                table: "Lessons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Lessons",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "RemovedTime",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Lessons");
        }
    }
}
