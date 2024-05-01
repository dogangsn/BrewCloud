using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updateData_29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetexamination",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    customerid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patientid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bodytemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pulse = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    respiratoryrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    symptomsid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    complaintstory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    treatmentdescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recid = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("VetExamination_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetvaccine",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    animaltype = table.Column<int>(type: "int", nullable: false),
                    vaccinename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timedone = table.Column<int>(type: "int", nullable: false),
                    renewaloption = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    obligation = table.Column<int>(type: "int", nullable: false),
                    totalsaleamount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    recid = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("VetVaccine_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetvaccinemedicine",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vaccineid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    salesamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxisid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dosingtype = table.Column<int>(type: "int", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vetvaccineid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recid = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("VetVaccineMedicine_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_vetvaccinemedicine_vetvaccine_vetvaccineid",
                        column: x => x.vetvaccineid,
                        principalTable: "vetvaccine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_vetvaccinemedicine_vetvaccineid",
                table: "vetvaccinemedicine",
                column: "vetvaccineid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetexamination");

            migrationBuilder.DropTable(
                name: "vetvaccinemedicine");

            migrationBuilder.DropTable(
                name: "vetvaccine");
        }
    }
}
