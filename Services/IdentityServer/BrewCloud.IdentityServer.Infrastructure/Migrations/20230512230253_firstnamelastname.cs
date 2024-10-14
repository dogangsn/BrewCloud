using Microsoft.EntityFrameworkCore.Migrations;

namespace BrewCloud.IdentityServer.Infrastructure.Migrations
{
    public partial class firstnamelastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "SubscriptionAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "SubscriptionAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "SubscriptionAccounts");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "SubscriptionAccounts");
        }
    }
}
