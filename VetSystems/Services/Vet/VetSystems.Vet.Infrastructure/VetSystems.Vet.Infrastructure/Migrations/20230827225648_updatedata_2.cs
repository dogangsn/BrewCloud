using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedata_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "animaltype",
                table: "vetproducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numberrepetitions",
                table: "vetproducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vetparameters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    autosms = table.Column<bool>(type: "bit", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateusers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createusers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetParameters_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetparameters");

            migrationBuilder.DropColumn(
                name: "animaltype",
                table: "vetproducts");

            migrationBuilder.DropColumn(
                name: "numberrepetitions",
                table: "vetproducts");

        }
    }
}
