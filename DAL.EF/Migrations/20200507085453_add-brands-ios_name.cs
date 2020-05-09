using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class addbrandsios_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsoBrandName",
                table: "Brand",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsoBrandName",
                table: "Brand");
        }
    }
}
