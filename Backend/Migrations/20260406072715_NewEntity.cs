using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class NewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KartLimit_Kartlar_KartId",
                table: "KartLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KartLimit",
                table: "KartLimit");

            migrationBuilder.RenameTable(
                name: "KartLimit",
                newName: "KartLimitleri");

            migrationBuilder.RenameIndex(
                name: "IX_KartLimit_KartId",
                table: "KartLimitleri",
                newName: "IX_KartLimitleri_KartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KartLimitleri",
                table: "KartLimitleri",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KartLimitleri_Kartlar_KartId",
                table: "KartLimitleri",
                column: "KartId",
                principalTable: "Kartlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KartLimitleri_Kartlar_KartId",
                table: "KartLimitleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KartLimitleri",
                table: "KartLimitleri");

            migrationBuilder.RenameTable(
                name: "KartLimitleri",
                newName: "KartLimit");

            migrationBuilder.RenameIndex(
                name: "IX_KartLimitleri_KartId",
                table: "KartLimit",
                newName: "IX_KartLimit_KartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KartLimit",
                table: "KartLimit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KartLimit_Kartlar_KartId",
                table: "KartLimit",
                column: "KartId",
                principalTable: "Kartlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
