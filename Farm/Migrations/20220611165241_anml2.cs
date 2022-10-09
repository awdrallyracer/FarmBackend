using Microsoft.EntityFrameworkCore.Migrations;

namespace Farm.Migrations
{
    public partial class anml2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodName",
                table: "Cares",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "Cares",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodName",
                table: "Cares");

            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "Cares");
        }
    }
}
