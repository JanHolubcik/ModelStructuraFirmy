using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrosUlohaJH.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Divizie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaId = table.Column<int>(type: "int", nullable: true),
                    VeduciRC = table.Column<string>(type: "nvarchar(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divizie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firmy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiaditelRc = table.Column<string>(type: "nvarchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oddelenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjektId = table.Column<int>(type: "int", nullable: true),
                    VeduciOddeleniaRc = table.Column<string>(type: "nvarchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oddelenia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zamestnanci",
                columns: table => new
                {
                    RodneCislo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Meno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priezvisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OddelenieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamestnanci", x => x.RodneCislo);
                    table.ForeignKey(
                        name: "FK_Zamestnanci_Oddelenia_OddelenieId",
                        column: x => x.OddelenieId,
                        principalTable: "Oddelenia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projekty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiviziaId = table.Column<int>(type: "int", nullable: false),
                    VeduciProjektuRC = table.Column<string>(type: "nvarchar(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekty_Divizie_DiviziaId",
                        column: x => x.DiviziaId,
                        principalTable: "Divizie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projekty_Zamestnanci_VeduciProjektuRC",
                        column: x => x.VeduciProjektuRC,
                        principalTable: "Zamestnanci",
                        principalColumn: "RodneCislo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divizie_FirmaId",
                table: "Divizie",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Divizie_VeduciRC",
                table: "Divizie",
                column: "VeduciRC",
                unique: true,
                filter: "[VeduciRC] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Firmy_RiaditelRc",
                table: "Firmy",
                column: "RiaditelRc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oddelenia_ProjektId",
                table: "Oddelenia",
                column: "ProjektId");

            migrationBuilder.CreateIndex(
                name: "IX_Oddelenia_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projekty_DiviziaId",
                table: "Projekty",
                column: "DiviziaId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekty_VeduciProjektuRC",
                table: "Projekty",
                column: "VeduciProjektuRC",
                unique: true,
                filter: "[VeduciProjektuRC] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Zamestnanci_OddelenieId",
                table: "Zamestnanci",
                column: "OddelenieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divizie_Firmy_FirmaId",
                table: "Divizie",
                column: "FirmaId",
                principalTable: "Firmy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Divizie_Zamestnanci_VeduciRC",
                table: "Divizie",
                column: "VeduciRC",
                principalTable: "Zamestnanci",
                principalColumn: "RodneCislo");

            migrationBuilder.AddForeignKey(
                name: "FK_Firmy_Zamestnanci_RiaditelRc",
                table: "Firmy",
                column: "RiaditelRc",
                principalTable: "Zamestnanci",
                principalColumn: "RodneCislo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Oddelenia_Projekty_ProjektId",
                table: "Oddelenia",
                column: "ProjektId",
                principalTable: "Projekty",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oddelenia_Zamestnanci_VeduciOddeleniaRc",
                table: "Oddelenia",
                column: "VeduciOddeleniaRc",
                principalTable: "Zamestnanci",
                principalColumn: "RodneCislo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divizie_Firmy_FirmaId",
                table: "Divizie");

            migrationBuilder.DropForeignKey(
                name: "FK_Divizie_Zamestnanci_VeduciRC",
                table: "Divizie");

            migrationBuilder.DropForeignKey(
                name: "FK_Oddelenia_Zamestnanci_VeduciOddeleniaRc",
                table: "Oddelenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Projekty_Zamestnanci_VeduciProjektuRC",
                table: "Projekty");

            migrationBuilder.DropTable(
                name: "Firmy");

            migrationBuilder.DropTable(
                name: "Zamestnanci");

            migrationBuilder.DropTable(
                name: "Oddelenia");

            migrationBuilder.DropTable(
                name: "Projekty");

            migrationBuilder.DropTable(
                name: "Divizie");
        }
    }
}
