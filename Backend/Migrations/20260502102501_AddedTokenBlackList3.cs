using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankaSimulasyon.Migrations
{
    /// <inheritdoc />
    public partial class AddedTokenBlackList3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpireTime",
                table: "TokenBlackLists",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpireTime",
                table: "TokenBlackLists",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
