using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.EF.Migrations
{
    public partial class initalaccessLvelconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccessLevel_RoleId",
                table: "AccessLevel",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessLevel_Role_RoleId",
                table: "AccessLevel",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessLevel_Role_RoleId",
                table: "AccessLevel");

            migrationBuilder.DropIndex(
                name: "IX_AccessLevel_RoleId",
                table: "AccessLevel");
        }
    }
}
