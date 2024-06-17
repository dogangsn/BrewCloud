using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updateData_45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "accomodationid",
                table: "vetsalebuyowner",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "examinationsid",
                table: "vetsalebuyowner",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isaccomodation",
                table: "vetsalebuyowner",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isexaminations",
                table: "vetsalebuyowner",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accomodationid",
                table: "vetsalebuyowner");

            migrationBuilder.DropColumn(
                name: "examinationsid",
                table: "vetsalebuyowner");

            migrationBuilder.DropColumn(
                name: "isaccomodation",
                table: "vetsalebuyowner");

            migrationBuilder.DropColumn(
                name: "isexaminations",
                table: "vetsalebuyowner");
        }
    }
}
