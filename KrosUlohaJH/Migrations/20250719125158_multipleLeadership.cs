using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrosUlohaJH.Migrations
{
    /// <inheritdoc />
    public partial class multipleLeadership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projekty_VeduciProjektuRC",
                table: "Projekty");

            migrationBuilder.DropIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.CreateIndex(
                name: "IX_Projekty_VeduciProjektuRC",
                table: "Projekty",
                column: "VeduciProjektuRC");

            migrationBuilder.CreateIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projekty_VeduciProjektuRC",
                table: "Projekty");

            migrationBuilder.DropIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.CreateIndex(
                name: "IX_Projekty_VeduciProjektuRC",
                table: "Projekty",
                column: "VeduciProjektuRC",
                unique: true,
                filter: "[VeduciProjektuRC] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                unique: true,
                filter: "[VeduciOddeleniaRc] IS NOT NULL");
        }
    }
}
