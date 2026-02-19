using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class KullaniciVeHesapEklendi2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HesapNumarasi = table.Column<int>(type: "INTEGER", nullable: false),
                    Isim = table.Column<string>(type: "TEXT", nullable: false),
                    Soyisim = table.Column<string>(type: "TEXT", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "TEXT", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", nullable: false),
                    Cinsiyet = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciHesaplari",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kullaniciId = table.Column<int>(type: "INTEGER", nullable: false),
                    HesapNumarasi = table.Column<int>(type: "INTEGER", nullable: false),
                    Bakiye = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciHesaplari", x => x.id);
                    table.ForeignKey(
                        name: "FK_KullaniciHesaplari_Kullanicilar_kullaniciId",
                        column: x => x.kullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciHesaplari_kullaniciId",
                table: "KullaniciHesaplari",
                column: "kullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciHesaplari");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
