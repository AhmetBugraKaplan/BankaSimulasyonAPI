using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class AddedKartKullanilanLimit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MusteriId",
                table: "Hesaplar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hesaplar_MusteriId",
                table: "Hesaplar",
                column: "MusteriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hesaplar_Musteriler_MusteriId",
                table: "Hesaplar",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hesaplar_Musteriler_MusteriId",
                table: "Hesaplar");

            migrationBuilder.DropIndex(
                name: "IX_Hesaplar_MusteriId",
                table: "Hesaplar");

            migrationBuilder.DropColumn(
                name: "MusteriId",
                table: "Hesaplar");
        }
    }
}
