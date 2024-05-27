using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Mail.Infrastructure.Migrations
{
    public partial class updateData_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "defaults",
                table: "smtpsettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "defaults",
                table: "smtpsettings");
        }
    }
}
