using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademyManagement.Persistence.Migrations.IdentityDatabase
{
    public partial class Add_Manually_Audit_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                schema: "Identity",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedTime",
                schema: "Identity",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                schema: "Identity",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertTime",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RemovedTime",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                schema: "Identity",
                table: "Users");
        }
    }
}
