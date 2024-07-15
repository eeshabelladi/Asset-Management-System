using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employeeSample.API.Migrations
{
    /// <inheritdoc />
    public partial class renameAndChangedAssetProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "ASSETS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WarrantyStartDate",
                table: "ASSETS",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WarrantyEndDate",
                table: "ASSETS",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isAvailable",
                table: "ASSETS",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAvailable",
                table: "ASSETS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WarrantyStartDate",
                table: "ASSETS",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WarrantyEndDate",
                table: "ASSETS",
                type: "datetime2(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatus",
                table: "ASSETS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
