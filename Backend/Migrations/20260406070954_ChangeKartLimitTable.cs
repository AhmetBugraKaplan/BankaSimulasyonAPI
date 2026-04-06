using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKartLimitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Limit",
                table: "KartLimit",
                newName: "KartKalanLimit");

            migrationBuilder.RenameColumn(
                name: "KalanLimit",
                table: "KartLimit",
                newName: "KartGunlukLimit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KartKalanLimit",
                table: "KartLimit",
                newName: "Limit");

            migrationBuilder.RenameColumn(
                name: "KartGunlukLimit",
                table: "KartLimit",
                newName: "KalanLimit");
        }
    }
}
