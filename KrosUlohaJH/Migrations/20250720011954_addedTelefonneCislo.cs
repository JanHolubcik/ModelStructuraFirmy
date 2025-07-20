using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KrosUlohaJH.Migrations
{
    /// <inheritdoc />
    public partial class addedTelefonneCislo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelefonneCislo",
                table: "Zamestnanci",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelefonneCislo",
                table: "Zamestnanci");
        }
    }
}
