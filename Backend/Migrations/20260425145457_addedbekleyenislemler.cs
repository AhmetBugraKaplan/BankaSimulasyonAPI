using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class addedbekleyenislemler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CebeGonderBekleyenIslemler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderenHesapNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    AliciTckNO = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    AliciTelNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GonderimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SonKabullenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CekilmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SmsOnayKodu = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CebeGonderBekleyenIslemler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CebeGonderBekleyenIslemler");
        }
    }
}
