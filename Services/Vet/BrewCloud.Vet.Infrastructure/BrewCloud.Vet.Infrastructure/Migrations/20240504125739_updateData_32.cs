using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "symptomsid",
                table: "vetexamination");

            migrationBuilder.AddColumn<string>(
                name: "symptoms",
                table: "vetexamination",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "symptoms",
                table: "vetexamination");

            migrationBuilder.AddColumn<Guid>(
                name: "symptomsid",
                table: "vetexamination",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
