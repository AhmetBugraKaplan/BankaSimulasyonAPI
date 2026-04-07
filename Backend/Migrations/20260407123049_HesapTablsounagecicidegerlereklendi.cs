using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class HesapTablsounagecicidegerlereklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HesapTip",
                table: "Hesaplar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParaBirimi",
                table: "Hesaplar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HesapTip",
                table: "Hesaplar");

            migrationBuilder.DropColumn(
                name: "ParaBirimi",
                table: "Hesaplar");
        }
    }
}
