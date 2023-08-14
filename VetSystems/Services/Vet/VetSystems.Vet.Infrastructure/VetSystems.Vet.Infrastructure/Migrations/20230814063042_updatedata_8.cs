using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedata_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createusers",
                table: "units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleteddate",
                table: "units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "deletedusers",
                table: "units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateusers",
                table: "units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createusers",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleteddate",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "deletedusers",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateusers",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createusers",
                table: "productcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleteddate",
                table: "productcategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "deletedusers",
                table: "productcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateusers",
                table: "productcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createusers",
                table: "patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleteddate",
                table: "patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "deletedusers",
                table: "patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateusers",
                table: "patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createusers",
                table: "customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleteddate",
                table: "customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "deletedusers",
                table: "customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateusers",
                table: "customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createusers",
                table: "adress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleteddate",
                table: "adress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "deletedusers",
                table: "adress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateusers",
                table: "adress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createusers",
                table: "units");

            migrationBuilder.DropColumn(
                name: "deleteddate",
                table: "units");

            migrationBuilder.DropColumn(
                name: "deletedusers",
                table: "units");

            migrationBuilder.DropColumn(
                name: "updateusers",
                table: "units");

            migrationBuilder.DropColumn(
                name: "createusers",
                table: "products");

            migrationBuilder.DropColumn(
                name: "deleteddate",
                table: "products");

            migrationBuilder.DropColumn(
                name: "deletedusers",
                table: "products");

            migrationBuilder.DropColumn(
                name: "updateusers",
                table: "products");

            migrationBuilder.DropColumn(
                name: "createusers",
                table: "productcategories");

            migrationBuilder.DropColumn(
                name: "deleteddate",
                table: "productcategories");

            migrationBuilder.DropColumn(
                name: "deletedusers",
                table: "productcategories");

            migrationBuilder.DropColumn(
                name: "updateusers",
                table: "productcategories");

            migrationBuilder.DropColumn(
                name: "createusers",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "deleteddate",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "deletedusers",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "updateusers",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "createusers",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "deleteddate",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "deletedusers",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "updateusers",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "createusers",
                table: "adress");

            migrationBuilder.DropColumn(
                name: "deleteddate",
                table: "adress");

            migrationBuilder.DropColumn(
                name: "deletedusers",
                table: "adress");

            migrationBuilder.DropColumn(
                name: "updateusers",
                table: "adress");
        }
    }
}
