using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VetSystems.IdentityServer.Infrastructure.Migrations
{
    public partial class createTempAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Accounts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubscriptionAccounts",
                columns: table => new
                {
                    Recid = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ActivationCode = table.Column<string>(nullable: true),
                    IsComplate = table.Column<bool>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    ConnectionString = table.Column<string>(nullable: true),
                    UseSafeListControl = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionAccounts", x => x.Recid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionAccounts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
