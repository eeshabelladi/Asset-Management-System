using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employeeSample.API.Migrations
{
    /// <inheritdoc />
    public partial class changedReqStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "REQUESTS");

            migrationBuilder.AddColumn<string>(
                name: "ReqStatus",
                table: "REQUESTS",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReqStatus",
                table: "REQUESTS");

            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "REQUESTS",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
