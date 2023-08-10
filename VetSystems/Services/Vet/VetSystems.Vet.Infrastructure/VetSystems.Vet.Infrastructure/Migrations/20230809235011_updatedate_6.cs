using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedate_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "buyingincludekdv",
                table: "products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "buyingprice",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "categoryid",
                table: "products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "criticalamount",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "fixprice",
                table: "products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isexpirationdate",
                table: "products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productbarcode",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productcode",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "producttypeid",
                table: "products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ratio",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "sellingincludekdv",
                table: "products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "sellingprice",
                table: "products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "supplierid",
                table: "products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "unitid",
                table: "products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "productcategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categorycode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductCategories_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productcategories");

            migrationBuilder.DropColumn(
                name: "active",
                table: "products");

            migrationBuilder.DropColumn(
                name: "buyingincludekdv",
                table: "products");

            migrationBuilder.DropColumn(
                name: "buyingprice",
                table: "products");

            migrationBuilder.DropColumn(
                name: "categoryid",
                table: "products");

            migrationBuilder.DropColumn(
                name: "criticalamount",
                table: "products");

            migrationBuilder.DropColumn(
                name: "fixprice",
                table: "products");

            migrationBuilder.DropColumn(
                name: "isexpirationdate",
                table: "products");

            migrationBuilder.DropColumn(
                name: "productbarcode",
                table: "products");

            migrationBuilder.DropColumn(
                name: "productcode",
                table: "products");

            migrationBuilder.DropColumn(
                name: "producttypeid",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ratio",
                table: "products");

            migrationBuilder.DropColumn(
                name: "sellingincludekdv",
                table: "products");

            migrationBuilder.DropColumn(
                name: "sellingprice",
                table: "products");

            migrationBuilder.DropColumn(
                name: "supplierid",
                table: "products");

            migrationBuilder.DropColumn(
                name: "unitid",
                table: "products");
        }
    }
}
