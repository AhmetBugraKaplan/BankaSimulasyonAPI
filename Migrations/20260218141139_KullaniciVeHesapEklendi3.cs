using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciVeHesapEklendi3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kullanicilar",
                columns: new[] { "id", "Adres", "Cinsiyet", "HesapNumarasi", "Isim", "Soyisim", "TelefonNumarasi" },
                values: new object[] { 1, "Zeytinburnu", "Cinsiyet girilmedi", 0, "BugraTest", "Kaplan", "Telefon numarası girilmedi" });

            migrationBuilder.InsertData(
                table: "KullaniciHesaplari",
                columns: new[] { "id", "Bakiye", "HesapNumarasi", "kullaniciId" },
                values: new object[] { 1, 100000m, 0, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KullaniciHesaplari",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kullanicilar",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
