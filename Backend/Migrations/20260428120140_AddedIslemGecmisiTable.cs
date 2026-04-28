using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class AddedIslemGecmisiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IslemGecmisleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HesapNumara = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KartsiTarafHesapNumara = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IslemTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemYonu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemTarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IslemSonrasiBakiye = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AtmID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslemGecmisleri", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IslemGecmisleri");
        }
    }
}
