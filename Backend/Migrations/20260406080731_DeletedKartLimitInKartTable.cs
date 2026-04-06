using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class DeletedKartLimitInKartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KartGunlukLimit",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "KartKalanLimit",
                table: "Kartlar");

            migrationBuilder.DropColumn(
                name: "KartKullanilanLimit",
                table: "Kartlar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "KartGunlukLimit",
                table: "Kartlar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "KartKalanLimit",
                table: "Kartlar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "KartKullanilanLimit",
                table: "Kartlar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
