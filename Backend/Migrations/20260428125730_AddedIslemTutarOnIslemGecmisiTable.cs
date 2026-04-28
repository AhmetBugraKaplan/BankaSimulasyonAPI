using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class AddedIslemTutarOnIslemGecmisiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "IslemTarihi",
                table: "IslemGecmisleri",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "IslemTutar",
                table: "IslemGecmisleri",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IslemTutar",
                table: "IslemGecmisleri");

            migrationBuilder.AlterColumn<string>(
                name: "IslemTarihi",
                table: "IslemGecmisleri",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
