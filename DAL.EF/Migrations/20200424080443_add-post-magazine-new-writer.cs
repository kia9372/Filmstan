using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class addpostmagazinenewwriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WriterId",
                table: "PostMagazine",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PostMagazine_WriterId",
                table: "PostMagazine",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostMagazine_User_WriterId",
                table: "PostMagazine",
                column: "WriterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostMagazine_User_WriterId",
                table: "PostMagazine");

            migrationBuilder.DropIndex(
                name: "IX_PostMagazine_WriterId",
                table: "PostMagazine");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "PostMagazine");
        }
    }
}
