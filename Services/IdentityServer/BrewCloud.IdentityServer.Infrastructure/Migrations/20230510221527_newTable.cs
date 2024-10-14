using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrewCloud.IdentityServer.Infrastructure.Migrations
{
    public partial class newTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppKey",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AuthorizeEnterprise",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VknNumber",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubscriptionSafeList",
                columns: table => new
                {
                    Recid = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    EnterpriseId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ControlType = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Action = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionSafeList", x => x.Recid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionSafeList");

            migrationBuilder.DropColumn(
                name: "AppKey",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AuthorizeEnterprise",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "VknNumber",
                table: "Accounts");
        }
    }
}
