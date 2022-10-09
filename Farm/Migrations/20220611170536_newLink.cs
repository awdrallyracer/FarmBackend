using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Farm.Migrations
{
    public partial class newLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InjectionId",
                table: "Cares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InjectionTime",
                table: "Cares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isInjection",
                table: "Cares",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Cares_InjectionId",
                table: "Cares",
                column: "InjectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cares_Injections_InjectionId",
                table: "Cares",
                column: "InjectionId",
                principalTable: "Injections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cares_Injections_InjectionId",
                table: "Cares");

            migrationBuilder.DropIndex(
                name: "IX_Cares_InjectionId",
                table: "Cares");

            migrationBuilder.DropColumn(
                name: "InjectionId",
                table: "Cares");

            migrationBuilder.DropColumn(
                name: "InjectionTime",
                table: "Cares");

            migrationBuilder.DropColumn(
                name: "isInjection",
                table: "Cares");
        }
    }
}
