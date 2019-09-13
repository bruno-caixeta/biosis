using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biosis.Migrations
{
    public partial class V2_AddField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "CORE",
                table: "Research",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "CORE",
                table: "Research",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                schema: "CORE",
                table: "Research",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "CORE",
                table: "Research");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "CORE",
                table: "Research");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                schema: "CORE",
                table: "Research");
        }
    }
}
