using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMAXLIMIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MAX_HesapLimit",
                table: "KullaniciHesaplari",
                newName: "HesapLimit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HesapLimit",
                table: "KullaniciHesaplari",
                newName: "MAX_HesapLimit");
        }
    }
}
