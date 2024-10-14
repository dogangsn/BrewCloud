using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updatedata_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "orderid",
                table: "vetsalebuytrans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "vatamount",
                table: "vetsalebuytrans",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "vatincluded",
                table: "vetsalebuytrans",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderid",
                table: "vetsalebuytrans");

            migrationBuilder.DropColumn(
                name: "vatamount",
                table: "vetsalebuytrans");

            migrationBuilder.DropColumn(
                name: "vatincluded",
                table: "vetsalebuytrans");
        }
    }
}
