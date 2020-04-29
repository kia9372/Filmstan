using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class addpostmagazinenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "PostMagazine",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PostMagazine_CategoryId",
                table: "PostMagazine",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostMagazine_Category_CategoryId",
                table: "PostMagazine",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostMagazine_Category_CategoryId",
                table: "PostMagazine");

            migrationBuilder.DropIndex(
                name: "IX_PostMagazine_CategoryId",
                table: "PostMagazine");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PostMagazine");
        }
    }
}
