using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class categorypropetyadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    PropName = table.Column<string>(nullable: true),
                    CategoryPropertyType_CategoryPropertyType = table.Column<int>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryProperty_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProperty_CategoryId",
                table: "CategoryProperty",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProperty");
        }
    }
}
