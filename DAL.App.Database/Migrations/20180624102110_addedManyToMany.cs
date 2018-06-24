using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.Database.Migrations
{
    public partial class addedManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SymptomsInDiseases",
                table: "SymptomsInDiseases");

            migrationBuilder.DropColumn(
                name: "id",
                table: "SymptomsInDiseases");

            migrationBuilder.RenameColumn(
                name: "DiseasId",
                table: "SymptomsInDiseases",
                newName: "SymptomsInDiseasesId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Symptoms",
                newName: "SymptomId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Diseases",
                newName: "DiseaseId");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "SymptomsInDiseases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SymptomsInDiseases_DiseaseId_SymptomId",
                table: "SymptomsInDiseases",
                columns: new[] { "DiseaseId", "SymptomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SymptomsInDiseases",
                table: "SymptomsInDiseases",
                columns: new[] { "SymptomId", "DiseaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomsInDiseases_Diseases_DiseaseId",
                table: "SymptomsInDiseases",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomsInDiseases_Symptoms_SymptomId",
                table: "SymptomsInDiseases",
                column: "SymptomId",
                principalTable: "Symptoms",
                principalColumn: "SymptomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SymptomsInDiseases_Diseases_DiseaseId",
                table: "SymptomsInDiseases");

            migrationBuilder.DropForeignKey(
                name: "FK_SymptomsInDiseases_Symptoms_SymptomId",
                table: "SymptomsInDiseases");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SymptomsInDiseases_DiseaseId_SymptomId",
                table: "SymptomsInDiseases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SymptomsInDiseases",
                table: "SymptomsInDiseases");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "SymptomsInDiseases");

            migrationBuilder.RenameColumn(
                name: "SymptomsInDiseasesId",
                table: "SymptomsInDiseases",
                newName: "DiseasId");

            migrationBuilder.RenameColumn(
                name: "SymptomId",
                table: "Symptoms",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DiseaseId",
                table: "Diseases",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "SymptomsInDiseases",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SymptomsInDiseases",
                table: "SymptomsInDiseases",
                column: "id");
        }
    }
}
