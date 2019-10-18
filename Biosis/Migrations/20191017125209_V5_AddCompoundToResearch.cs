using Microsoft.EntityFrameworkCore.Migrations;

namespace Biosis.Migrations
{
    public partial class V5_AddCompoundToResearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalManchas",
                schema: "CORE",
                table: "TransData",
                newName: "TaintTotal");

            migrationBuilder.RenameColumn(
                name: "NumeroIndividuos",
                schema: "CORE",
                table: "TransData",
                newName: "PopulationNumber");

            migrationBuilder.RenameColumn(
                name: "IsControle",
                schema: "CORE",
                table: "TransData",
                newName: "IsControl");

            migrationBuilder.RenameColumn(
                name: "DiagnosticoEstatistico",
                schema: "CORE",
                table: "TransData",
                newName: "StatisticDiagnose");

            migrationBuilder.RenameColumn(
                name: "Cruzamento",
                schema: "CORE",
                table: "TransData",
                newName: "Compound");

            migrationBuilder.RenameColumn(
                name: "Composto",
                schema: "CORE",
                table: "TransData",
                newName: "Breed");

            migrationBuilder.AddColumn<string>(
                name: "Compound",
                schema: "CORE",
                table: "Research",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compound",
                schema: "CORE",
                table: "Research");

            migrationBuilder.RenameColumn(
                name: "TaintTotal",
                schema: "CORE",
                table: "TransData",
                newName: "TotalManchas");

            migrationBuilder.RenameColumn(
                name: "StatisticDiagnose",
                schema: "CORE",
                table: "TransData",
                newName: "DiagnosticoEstatistico");

            migrationBuilder.RenameColumn(
                name: "PopulationNumber",
                schema: "CORE",
                table: "TransData",
                newName: "NumeroIndividuos");

            migrationBuilder.RenameColumn(
                name: "IsControl",
                schema: "CORE",
                table: "TransData",
                newName: "IsControle");

            migrationBuilder.RenameColumn(
                name: "Compound",
                schema: "CORE",
                table: "TransData",
                newName: "Cruzamento");

            migrationBuilder.RenameColumn(
                name: "Breed",
                schema: "CORE",
                table: "TransData",
                newName: "Composto");
        }
    }
}
