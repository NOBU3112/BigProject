using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class _12312 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaTV",
                table: "users",
                newName: "MaSV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaSV",
                table: "users",
                newName: "MaTV");
        }
    }
}
