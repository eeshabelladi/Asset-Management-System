using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace employeeSample.API.Migrations
{
    /// <inheritdoc />
    public partial class changedGidType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GID",
                table: "EMPLOYEEMASTER",
                newName: "Gid");

            migrationBuilder.AlterColumn<string>(
                name: "Gid",
                table: "EMPLOYEEMASTER",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gid",
                table: "EMPLOYEEMASTER",
                newName: "GID");

            migrationBuilder.AlterColumn<int>(
                name: "GID",
                table: "EMPLOYEEMASTER",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");
        }
    }
}
