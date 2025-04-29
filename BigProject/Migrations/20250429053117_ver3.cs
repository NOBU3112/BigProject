using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "memberInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "memberInfos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Major",
                value: null);

            migrationBuilder.UpdateData(
                table: "memberInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Major",
                value: null);

            migrationBuilder.UpdateData(
                table: "memberInfos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Major",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Major",
                table: "memberInfos");
        }
    }
}
