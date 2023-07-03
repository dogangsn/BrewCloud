using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updateData_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "products");
        }
    }
}
