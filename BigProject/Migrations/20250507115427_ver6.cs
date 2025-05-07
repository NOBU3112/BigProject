using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigProject.Migrations
{
    /// <inheritdoc />
    public partial class ver6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rewardDisciplines_users_RecipientId",
                table: "rewardDisciplines");

            migrationBuilder.DropIndex(
                name: "IX_rewardDisciplines_RecipientId",
                table: "rewardDisciplines");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "rewardDisciplines");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "rewardDisciplines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlFile",
                table: "rewardDisciplines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "rewardDisciplines");

            migrationBuilder.DropColumn(
                name: "UrlFile",
                table: "rewardDisciplines");

            migrationBuilder.AddColumn<int>(
                name: "RecipientId",
                table: "rewardDisciplines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_rewardDisciplines_RecipientId",
                table: "rewardDisciplines",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_rewardDisciplines_users_RecipientId",
                table: "rewardDisciplines",
                column: "RecipientId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
