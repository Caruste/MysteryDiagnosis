using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.Database.Migrations
{
    public partial class ChangedABit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_Diseases_DiseaseId",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_DiseaseId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "Symptoms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "Symptoms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_DiseaseId",
                table: "Symptoms",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_Diseases_DiseaseId",
                table: "Symptoms",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
