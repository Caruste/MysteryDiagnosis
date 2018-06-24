using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.Database.Migrations
{
    public partial class fixedNamingProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SymptomsInDiseases_DiseaseId_SymptomId",
                table: "SymptomsInDiseases");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Symptoms",
                newName: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SymptomsInDiseases_SymptomsInDiseasesId",
                table: "SymptomsInDiseases",
                column: "SymptomsInDiseasesId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomsInDiseases_DiseaseId",
                table: "SymptomsInDiseases",
                column: "DiseaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SymptomsInDiseases_SymptomsInDiseasesId",
                table: "SymptomsInDiseases");

            migrationBuilder.DropIndex(
                name: "IX_SymptomsInDiseases_DiseaseId",
                table: "SymptomsInDiseases");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Symptoms",
                newName: "name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SymptomsInDiseases_DiseaseId_SymptomId",
                table: "SymptomsInDiseases",
                columns: new[] { "DiseaseId", "SymptomId" });
        }
    }
}
