using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdCat.Migrations
{
    public partial class dbupdatenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProdName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CatName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProdName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CatName",
                table: "Categories",
                newName: "Name");
        }
    }
}
