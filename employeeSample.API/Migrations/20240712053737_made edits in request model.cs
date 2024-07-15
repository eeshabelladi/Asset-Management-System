using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employeeSample.API.Migrations
{
    /// <inheritdoc />
    public partial class madeeditsinrequestmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__REQUESTS__AssetI__5BAD9CC8",
                table: "REQUESTS");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "REQUESTS");

            migrationBuilder.AlterColumn<int>(
                name: "AssetID",
                table: "REQUESTS",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ReqAssetType",
                table: "REQUESTS",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "REQUESTS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK__REQUESTS__AssetI__5BAD9CC8",
                table: "REQUESTS",
                column: "AssetID",
                principalTable: "ASSETS",
                principalColumn: "AssetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__REQUESTS__AssetI__5BAD9CC8",
                table: "REQUESTS");

            migrationBuilder.DropColumn(
                name: "ReqAssetType",
                table: "REQUESTS");

            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "REQUESTS");

            migrationBuilder.AlterColumn<int>(
                name: "AssetID",
                table: "REQUESTS",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestStatus",
                table: "REQUESTS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK__REQUESTS__AssetI__5BAD9CC8",
                table: "REQUESTS",
                column: "AssetID",
                principalTable: "ASSETS",
                principalColumn: "AssetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
