using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Account.Infrastructure.Migrations
{
    public partial class UpdateData_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "firstlastname",
                table: "users",
                newName: "updateuser");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "firstname",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "userauthorization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "userauthorization",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "userauthorization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "tempaccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "tempaccount",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "tempaccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "rolesettingdetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "rolesettingdetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "rolesettingdetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "rolesetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "rolesetting",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "rolesetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "reasonproperties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "reasonproperties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "reasonproperties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "reason",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "reason",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "reason",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "property",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "enterprise",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "enterprise",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "enterprise",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createuser",
                table: "abilitygroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "abilitygroup",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "updateuser",
                table: "abilitygroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    companycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companytitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tradename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxnumber = table.Column<int>(type: "int", nullable: true),
                    taxoffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    defaultinvoicetype = table.Column<int>(type: "int", nullable: true),
                    companyimage = table.Column<byte>(type: "tinyint", nullable: true),
                    buildingname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    buildingnumber = table.Column<int>(type: "int", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    invoiceamountnotes = table.Column<bool>(type: "bit", nullable: true),
                    invoicenoautocreate = table.Column<bool>(type: "bit", nullable: true),
                    invoicesendemail = table.Column<bool>(type: "bit", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createuser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateuser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropColumn(
                name: "active",
                table: "users");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "users");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "users");

            migrationBuilder.DropColumn(
                name: "firstname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "lastname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "userauthorization");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "userauthorization");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "userauthorization");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "tempaccount");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "tempaccount");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "tempaccount");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "rolesettingdetail");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "rolesettingdetail");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "rolesettingdetail");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "rolesetting");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "rolesetting");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "rolesetting");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "reasonproperties");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "reasonproperties");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "reasonproperties");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "reason");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "reason");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "reason");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "property");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "property");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "property");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "enterprise");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "enterprise");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "enterprise");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "createuser",
                table: "abilitygroup");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "abilitygroup");

            migrationBuilder.DropColumn(
                name: "updateuser",
                table: "abilitygroup");

            migrationBuilder.RenameColumn(
                name: "updateuser",
                table: "users",
                newName: "firstlastname");
        }
    }
}
