using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKullaniciHesapId_withh_KullaniciId_in_Kartlar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_KullaniciHesaplari_KullaniciHesapId",
                table: "Kartlar");

            migrationBuilder.RenameColumn(
                name: "KullaniciHesapId",
                table: "Kartlar",
                newName: "kullaniciHesapid");

            migrationBuilder.RenameIndex(
                name: "IX_Kartlar_KullaniciHesapId",
                table: "Kartlar",
                newName: "IX_Kartlar_kullaniciHesapid");

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId",
                table: "Kartlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_KullaniciHesaplari_kullaniciHesapid",
                table: "Kartlar",
                column: "kullaniciHesapid",
                principalTable: "KullaniciHesaplari",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kartlar_KullaniciHesaplari_kullaniciHesapid",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Kartlar");

            migrationBuilder.RenameColumn(
                name: "kullaniciHesapid",
                table: "Kartlar",
                newName: "KullaniciHesapId");

            migrationBuilder.RenameIndex(
                name: "IX_Kartlar_kullaniciHesapid",
                table: "Kartlar",
                newName: "IX_Kartlar_KullaniciHesapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kartlar_KullaniciHesaplari_KullaniciHesapId",
                table: "Kartlar",
                column: "KullaniciHesapId",
                principalTable: "KullaniciHesaplari",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
