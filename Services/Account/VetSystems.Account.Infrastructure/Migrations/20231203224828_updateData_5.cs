using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Account.Infrastructure.Migrations
{
    public partial class updateData_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isappointmentshow",
                table: "titledefinitions",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isappointmentshow",
                table: "titledefinitions");
        }
    }
}
