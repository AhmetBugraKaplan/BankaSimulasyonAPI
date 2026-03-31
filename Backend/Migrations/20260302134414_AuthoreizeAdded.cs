using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class AuthoreizeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AtmKasetler",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AtmLer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kartlar",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AtmLer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "KullaniciHesaplari",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kullanicilar",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KullaniciRol",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "KullaniciRol",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Kullanicilar");

            migrationBuilder.InsertData(
                table: "AtmLer",
                columns: new[] { "Id", "AktifMi", "Konum" },
                values: new object[,]
                {
                    { 1, true, "Zeytinburnu Beştelsiz Şube" },
                    { 2, false, "Bakırköy Cadde Şube" }
                });

            migrationBuilder.InsertData(
                table: "Kullanicilar",
                columns: new[] { "id", "Adres", "Cinsiyet", "Isim", "Soyisim", "TelefonNumarasi" },
                values: new object[] { 1, "Zeytinburnu", "Cinsiyet girilmedi", "BugraTest", "Kaplan", "Telefon numarası girilmedi" });

            migrationBuilder.InsertData(
                table: "AtmKasetler",
                columns: new[] { "Id", "Adet", "AtmId", "KritikDeger", "Kupur", "SlotNumarasi" },
                values: new object[,]
                {
                    { 1, 200, 1, 20, 200, 1 },
                    { 2, 200, 1, 20, 100, 2 },
                    { 3, 200, 1, 20, 50, 3 },
                    { 4, 200, 1, 20, 20, 4 }
                });

            migrationBuilder.InsertData(
                table: "KullaniciHesaplari",
                columns: new[] { "id", "Bakiye", "HesapNumarasi", "KullaniciId", "Sifre" },
                values: new object[] { 1, 100000m, 1001, 1, "1234" });

            migrationBuilder.InsertData(
                table: "Kartlar",
                columns: new[] { "id", "AktifMi", "CVV", "KartNumara", "KartSKT", "KartTipi", "KullaniciHesapId" },
                values: new object[] { 1, true, "123", "6656 9988 1238 7435", "04/29", "Banka", 1 });
        }
    }
}
