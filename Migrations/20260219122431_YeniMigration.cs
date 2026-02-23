using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class YeniMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciHesaplari_Kullanicilar_kullaniciId",
                table: "KullaniciHesaplari");

            migrationBuilder.RenameColumn(
                name: "kullaniciId",
                table: "KullaniciHesaplari",
                newName: "KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_KullaniciHesaplari_kullaniciId",
                table: "KullaniciHesaplari",
                newName: "IX_KullaniciHesaplari_KullaniciId");

            migrationBuilder.AddColumn<string>(
                name: "Sifre",
                table: "KullaniciHesaplari",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 1,
                column: "Adet",
                value: 200);

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 2,
                column: "Adet",
                value: 200);

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 3,
                column: "Adet",
                value: 200);

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 4,
                column: "Adet",
                value: 200);

            migrationBuilder.UpdateData(
                table: "KullaniciHesaplari",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "HesapNumarasi", "Sifre" },
                values: new object[] { 1001, "1234" });

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciHesaplari_Kullanicilar_KullaniciId",
                table: "KullaniciHesaplari",
                column: "KullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciHesaplari_Kullanicilar_KullaniciId",
                table: "KullaniciHesaplari");

            migrationBuilder.DropColumn(
                name: "Sifre",
                table: "KullaniciHesaplari");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "KullaniciHesaplari",
                newName: "kullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_KullaniciHesaplari_KullaniciId",
                table: "KullaniciHesaplari",
                newName: "IX_KullaniciHesaplari_kullaniciId");

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 1,
                column: "Adet",
                value: 20);

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 2,
                column: "Adet",
                value: 20);

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 3,
                column: "Adet",
                value: 20);

            migrationBuilder.UpdateData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 4,
                column: "Adet",
                value: 20);

            migrationBuilder.UpdateData(
                table: "KullaniciHesaplari",
                keyColumn: "id",
                keyValue: 1,
                column: "HesapNumarasi",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciHesaplari_Kullanicilar_kullaniciId",
                table: "KullaniciHesaplari",
                column: "kullaniciId",
                principalTable: "Kullanicilar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
