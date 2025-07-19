using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrosUlohaJH.Migrations
{
    /// <inheritdoc />
    public partial class AddVedeneDivizieRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekty_Divizie_DiviziaId",
                table: "Projekty");

            migrationBuilder.DropIndex(
                name: "IX_Divizie_VeduciRC",
                table: "Divizie");

            migrationBuilder.AlterColumn<int>(
                name: "DiviziaId",
                table: "Projekty",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Divizie_VeduciRC",
                table: "Divizie",
                column: "VeduciRC");

            migrationBuilder.AddForeignKey(
                name: "FK_Projekty_Divizie_DiviziaId",
                table: "Projekty",
                column: "DiviziaId",
                principalTable: "Divizie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekty_Divizie_DiviziaId",
                table: "Projekty");

            migrationBuilder.DropIndex(
                name: "IX_Divizie_VeduciRC",
                table: "Divizie");

            migrationBuilder.AlterColumn<int>(
                name: "DiviziaId",
                table: "Projekty",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Divizie_VeduciRC",
                table: "Divizie",
                column: "VeduciRC",
                unique: true,
                filter: "[VeduciRC] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Projekty_Divizie_DiviziaId",
                table: "Projekty",
                column: "DiviziaId",
                principalTable: "Divizie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
