using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class AddedMaxLimitKalanLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HesapLimit",
                table: "KullaniciHesaplari",
                newName: "MAX_HesapLimit");

            migrationBuilder.AddColumn<decimal>(
                name: "KalanLimit",
                table: "KullaniciHesaplari",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KalanLimit",
                table: "KullaniciHesaplari");

            migrationBuilder.RenameColumn(
                name: "MAX_HesapLimit",
                table: "KullaniciHesaplari",
                newName: "HesapLimit");
        }
    }
}
