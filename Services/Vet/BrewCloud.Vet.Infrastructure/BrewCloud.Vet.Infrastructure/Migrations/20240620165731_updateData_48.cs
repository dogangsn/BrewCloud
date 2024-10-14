using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_48 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetvaccinecalendar",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    animaltype = table.Column<int>(type: "int", nullable: false),
                    vaccinedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isdone = table.Column<bool>(type: "bit", nullable: false),
                    isadd = table.Column<bool>(type: "bit", nullable: false),
                    patientid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customerid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccineid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccinename = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("VetVaccineCalendar_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetvaccinecalendar");
        }
    }
}
