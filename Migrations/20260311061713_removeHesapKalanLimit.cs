using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class removeHesapKalanLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KalanLimit",
                table: "KullaniciHesaplari");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "KalanLimit",
                table: "KullaniciHesaplari",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
