using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employeeSample.API.Migrations
{
    /// <inheritdoc />
    public partial class changedAssetAllocTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeallocationDate",
                table: "ASSET_ALLOCATION");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "ASSET_ALLOCATION",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ASSET_ALLOCATION");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeallocationDate",
                table: "ASSET_ALLOCATION",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);
        }
    }
}
