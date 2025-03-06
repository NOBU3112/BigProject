using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class vera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActiveAccount",
                table: "emailConfirms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActiveAccount",
                table: "emailConfirms");
        }
    }
}
