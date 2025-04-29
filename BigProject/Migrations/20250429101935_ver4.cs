using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class ver4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "memberInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "memberInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gender",
                value: null);

            migrationBuilder.UpdateData(
                table: "memberInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gender",
                value: null);

            migrationBuilder.UpdateData(
                table: "memberInfos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Gender",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "memberInfos");
        }
    }
}
