using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class KartEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kartlar",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KullaniciHesapId = table.Column<int>(type: "INTEGER", nullable: false),
                    KartNumara = table.Column<string>(type: "TEXT", nullable: false),
                    KartSKT = table.Column<string>(type: "TEXT", nullable: false),
                    CVV = table.Column<string>(type: "TEXT", nullable: false),
                    KartTipi = table.Column<string>(type: "TEXT", nullable: false),
                    AktifMi = table.Column<bool>(type: "INTEGER", nullable: false)
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
                table: "Kartlar",
                columns: new[] { "id", "AktifMi", "CVV", "KartNumara", "KartSKT", "KartTipi", "KullaniciHesapId" },
                values: new object[] { 1, true, "123", "6656 9988 1238 7435", "04/29", "Banka", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Kartlar_KullaniciHesapId",
                table: "Kartlar",
                column: "KullaniciHesapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kartlar");
        }
    }
}
