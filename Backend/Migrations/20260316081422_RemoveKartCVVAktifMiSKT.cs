using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class RemoveKartCVVAktifMiSKT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_Musteriler_KullaniciId",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "CVV",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "KartSKT",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "KartTipi",
                table: "Kartlar");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "Kartlar",
                newName: "MusteriId");

            migrationBuilder.RenameColumn(
                name: "KartLimit",
                table: "Kartlar",
                newName: "KartKalanLimit");

            migrationBuilder.RenameIndex(
                name: "IX_Kartlar_KullaniciId",
                table: "Kartlar",
                newName: "IX_Kartlar_MusteriId");

            migrationBuilder.AddColumn<decimal>(
                name: "KartGunlukLimit",
                table: "Kartlar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_Musteriler_MusteriId",
                table: "Kartlar",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_Musteriler_MusteriId",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "KartGunlukLimit",
                table: "Kartlar");

            migrationBuilder.RenameColumn(
                name: "MusteriId",
                table: "Kartlar",
                newName: "KullaniciId");

            migrationBuilder.RenameColumn(
                name: "KartKalanLimit",
                table: "Kartlar",
                newName: "KartLimit");

            migrationBuilder.RenameIndex(
                name: "IX_Kartlar_MusteriId",
                table: "Kartlar",
                newName: "IX_Kartlar_KullaniciId");

            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "Kartlar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CVV",
                table: "Kartlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KartSKT",
                table: "Kartlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KartTipi",
                table: "Kartlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_Musteriler_KullaniciId",
                table: "Kartlar",
                column: "KullaniciId",
                principalTable: "Musteriler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
