using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrosUlohaJH.Migrations
{
    /// <inheritdoc />
    public partial class EditedOddelenia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oddelenia_Zamestnanci_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.DropIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.AlterColumn<string>(
                name: "VeduciOddeleniaRc",
                table: "Oddelenia",
                type: "nvarchar(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)");

            migrationBuilder.CreateIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                unique: true,
                filter: "[VeduciOddeleniaRc] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Oddelenia_Zamestnanci_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                principalTable: "Zamestnanci",
                principalColumn: "RodneCislo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oddelenia_Zamestnanci_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.DropIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.AlterColumn<string>(
                name: "VeduciOddeleniaRc",
                table: "Oddelenia",
                type: "nvarchar(11)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Oddelenia_Zamestnanci_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                principalTable: "Zamestnanci",
                principalColumn: "RodneCislo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
