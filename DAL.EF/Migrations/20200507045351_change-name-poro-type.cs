using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class changenameporotype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryPropertyType_CategoryPropertyType",
                table: "CategoryProperty",
                newName: "CategoryPropertyType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryPropertyType",
                table: "CategoryProperty",
                newName: "CategoryPropertyType_CategoryPropertyType");
        }
    }
}
