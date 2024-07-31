using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updateData_63 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pethotelsdatetimeformat",
                table: "vetparameters",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pethotelsdatetimeformat",
                table: "vetparameters");
        }
    }
}
