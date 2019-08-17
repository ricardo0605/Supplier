using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class supplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Activo",
                table: "Suppliers",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Suppliers",
                newName: "Activo");
        }
    }
}
