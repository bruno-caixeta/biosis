using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biosis.Migrations
{
    public partial class V1_InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CORE");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "CORE",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Research",
                schema: "CORE",
                columns: table => new
                {
                    ResearchId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Research", x => x.ResearchId);
                    table.ForeignKey(
                        name: "FK_Research_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "CORE",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransData",
                schema: "CORE",
                columns: table => new
                {
                    TransDataId = table.Column<Guid>(nullable: false),
                    Composto = table.Column<string>(nullable: true),
                    Cruzamento = table.Column<string>(nullable: true),
                    Dose = table.Column<string>(nullable: true),
                    NumeroIndividuos = table.Column<int>(nullable: false),
                    MSG = table.Column<int>(nullable: false),
                    MSP = table.Column<int>(nullable: false),
                    MG = table.Column<int>(nullable: false),
                    TotalManchas = table.Column<int>(nullable: false),
                    IsControle = table.Column<bool>(nullable: false),
                    DiagnosticoEstatistico = table.Column<string>(nullable: true),
                    Class1 = table.Column<int>(nullable: false),
                    Class2 = table.Column<int>(nullable: false),
                    Class3 = table.Column<int>(nullable: false),
                    Class4 = table.Column<int>(nullable: false),
                    Class5 = table.Column<int>(nullable: false),
                    Class6 = table.Column<int>(nullable: false),
                    Class7 = table.Column<int>(nullable: false),
                    Class8 = table.Column<int>(nullable: false),
                    Class9 = table.Column<int>(nullable: false),
                    Class10 = table.Column<int>(nullable: false),
                    ResearchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransData", x => x.TransDataId);
                    table.ForeignKey(
                        name: "FK_TransData_Research_ResearchId",
                        column: x => x.ResearchId,
                        principalSchema: "CORE",
                        principalTable: "Research",
                        principalColumn: "ResearchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Research_UserId",
                schema: "CORE",
                table: "Research",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransData_ResearchId",
                schema: "CORE",
                table: "TransData",
                column: "ResearchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransData",
                schema: "CORE");

            migrationBuilder.DropTable(
                name: "Research",
                schema: "CORE");

            migrationBuilder.DropTable(
                name: "User",
                schema: "CORE");
        }
    }
}
