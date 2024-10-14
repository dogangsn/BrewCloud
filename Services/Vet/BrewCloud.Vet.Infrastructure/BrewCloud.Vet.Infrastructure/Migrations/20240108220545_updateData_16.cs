using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ispaymentreceived",
                table: "vetappointments",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ispaymentreceived",
                table: "vetappointments");
        }
    }
}
