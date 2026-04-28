using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class removetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmsOnayKodu",
                table: "CebeGonderBekleyenIslemler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmsOnayKodu",
                table: "CebeGonderBekleyenIslemler",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);
        }
    }
}
