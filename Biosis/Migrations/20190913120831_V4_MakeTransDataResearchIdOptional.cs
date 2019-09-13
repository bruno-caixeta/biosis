using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biosis.Migrations
{
    public partial class V4_MakeTransDataResearchIdOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ResearchId",
                schema: "CORE",
                table: "TransData",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ResearchId",
                schema: "CORE",
                table: "TransData",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
