using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biosis.Migrations
{
    public partial class V3_AddFieldControlIdToResearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ControlId",
                schema: "CORE",
                table: "Research",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlId",
                schema: "CORE",
                table: "Research");
        }
    }
}
