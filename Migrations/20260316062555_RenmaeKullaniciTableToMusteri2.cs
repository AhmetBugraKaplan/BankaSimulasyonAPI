using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class RenmaeKullaniciTableToMusteri2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_Kullanicilar_KullaniciId",
                table: "Kartlar");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciHesaplari_Kullanicilar_KullaniciId",
                table: "KullaniciHesaplari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar");

            migrationBuilder.RenameTable(
                name: "Kullanicilar",
                newName: "Musteriler");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_Musteriler_KullaniciId",
                table: "Kartlar",
                column: "KullaniciId",
                principalTable: "Musteriler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciHesaplari_Musteriler_KullaniciId",
                table: "KullaniciHesaplari",
                column: "KullaniciId",
                principalTable: "Musteriler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_Musteriler_KullaniciId",
                table: "Kartlar");

            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciHesaplari_Musteriler_KullaniciId",
                table: "KullaniciHesaplari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musteriler",
                table: "Musteriler");

            migrationBuilder.RenameTable(
                name: "Musteriler",
                newName: "Kullanicilar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kullanicilar",
                table: "Kullanicilar",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_Kullanicilar_KullaniciId",
                table: "Kartlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciHesaplari_Kullanicilar_KullaniciId",
                table: "KullaniciHesaplari",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
