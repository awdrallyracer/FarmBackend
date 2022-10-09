using Microsoft.EntityFrameworkCore.Migrations;

namespace Farm.Migrations
{
    public partial class careMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cares",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cares_UserId",
                table: "Cares",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cares_Users_UserId",
                table: "Cares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cares_Users_UserId",
                table: "Cares");

            migrationBuilder.DropIndex(
                name: "IX_Cares_UserId",
                table: "Cares");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cares");
        }
    }
}
