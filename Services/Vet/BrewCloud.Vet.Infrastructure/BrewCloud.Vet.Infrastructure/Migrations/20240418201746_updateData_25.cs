using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ratio",
                table: "vetappointmenttypes");

            migrationBuilder.AddColumn<Guid>(
                name: "taxisid",
                table: "vetappointmenttypes",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "taxisid",
                table: "vetappointmenttypes");

            migrationBuilder.AddColumn<decimal>(
                name: "ratio",
                table: "vetappointmenttypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
