using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_KullaniciHesaplari_kullaniciHesapid",
                table: "Kartlar");

            migrationBuilder.DropIndex(
                name: "IX_Kartlar_kullaniciHesapid",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "kullaniciHesapid",
                table: "Kartlar");

            migrationBuilder.CreateIndex(
                name: "IX_Kartlar_KullaniciId",
                table: "Kartlar",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_Kullanicilar_KullaniciId",
                table: "Kartlar",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_Kullanicilar_KullaniciId",
                table: "Kartlar");

            migrationBuilder.DropIndex(
                name: "IX_Kartlar_KullaniciId",
                table: "Kartlar");

            migrationBuilder.AddColumn<int>(
                name: "kullaniciHesapid",
                table: "Kartlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kartlar_kullaniciHesapid",
                table: "Kartlar",
                column: "kullaniciHesapid");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_KullaniciHesaplari_kullaniciHesapid",
                table: "Kartlar",
                column: "kullaniciHesapid",
                principalTable: "KullaniciHesaplari",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
