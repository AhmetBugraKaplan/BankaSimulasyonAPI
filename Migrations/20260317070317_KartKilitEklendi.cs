using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class KartKilitEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Musteriler",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "KartSifreleri",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Kartlar",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "YanlisGirisSayisi",
                table: "Kartlar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YanlisGirisSayisi",
                table: "Kartlar");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Musteriler",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "KartSifreleri",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kartlar",
                newName: "id");
        }
    }
}
