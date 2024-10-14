using Microsoft.EntityFrameworkCore.Migrations;

namespace BrewCloud.IdentityServer.Infrastructure.Migrations
{
    public partial class mig_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AccountType = table.Column<int>(nullable: false),
                    CompanyId = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    Passive = table.Column<bool>(nullable: true),
                    IsLicenceAccount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Accounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
