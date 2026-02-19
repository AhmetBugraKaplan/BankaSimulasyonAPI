using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class kritikDegerEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtmLer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Konum = table.Column<string>(type: "TEXT", nullable: false),
                    AktifMi = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtmLer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtmKasetler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AtmId = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotNumarasi = table.Column<int>(type: "INTEGER", nullable: false),
                    Kupur = table.Column<int>(type: "INTEGER", nullable: false),
                    Adet = table.Column<int>(type: "INTEGER", nullable: false),
                    KritikDeger = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.InsertData(
                table: "AtmLer",
                columns: new[] { "Id", "AktifMi", "Konum" },
                values: new object[,]
                {
                    { 1, true, "Zeytinburnu Beştelsiz Şube" },
                    { 2, false, "Bakırköy Cadde Şube" }
                });

            migrationBuilder.InsertData(
                table: "AtmKasetler",
                columns: new[] { "Id", "Adet", "AtmId", "KritikDeger", "Kupur", "SlotNumarasi" },
                values: new object[,]
                {
                    { 1, 20, 1, 20, 200, 1 },
                    { 2, 20, 1, 20, 100, 2 },
                    { 3, 20, 1, 20, 50, 3 },
                    { 4, 20, 1, 20, 20, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtmKasetler_AtmId",
                table: "AtmKasetler",
                column: "AtmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtmKasetler");

            migrationBuilder.DropTable(
                name: "AtmLer");
        }
    }
}
