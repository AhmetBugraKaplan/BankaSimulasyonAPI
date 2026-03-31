using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class InitilalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtmLer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Konum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtmLer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AtmKasetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtmId = table.Column<int>(type: "int", nullable: false),
                    SlotNumarasi = table.Column<int>(type: "int", nullable: false),
                    Kupur = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    KritikDeger = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtmKasetler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtmKasetler_AtmLer_AtmId",
                        column: x => x.AtmId,
                        principalTable: "AtmLer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciHesaplari",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    HesapNumarasi = table.Column<int>(type: "int", nullable: false),
                    Bakiye = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciHesaplari", x => x.id);
                    table.ForeignKey(
                        name: "FK_KullaniciHesaplari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kartlar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciHesapId = table.Column<int>(type: "int", nullable: false),
                    KartNumara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KartSKT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KartTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kartlar", x => x.id);
                    table.ForeignKey(
                        name: "FK_Kartlar_KullaniciHesaplari_KullaniciHesapId",
                        column: x => x.KullaniciHesapId,
                        principalTable: "KullaniciHesaplari",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AtmKasetler_AtmId",
                table: "AtmKasetler",
                column: "AtmId");

            migrationBuilder.CreateIndex(
                name: "IX_Kartlar_KullaniciHesapId",
                table: "Kartlar",
                column: "KullaniciHesapId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciHesaplari_KullaniciId",
                table: "KullaniciHesaplari",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtmKasetler");

            migrationBuilder.DropTable(
                name: "Kartlar");

            migrationBuilder.DropTable(
                name: "AtmLer");

            migrationBuilder.DropTable(
                name: "KullaniciHesaplari");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
