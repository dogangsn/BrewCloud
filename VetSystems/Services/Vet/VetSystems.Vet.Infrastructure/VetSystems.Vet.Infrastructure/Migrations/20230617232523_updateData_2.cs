using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updateData_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    county = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longadress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adress", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fisrname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber = table.Column<int>(type: "int", nullable: false),
                    phonenumber2 = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxoffice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vkntcno = table.Column<int>(type: "int", nullable: false),
                    customergroup = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discountrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isemail = table.Column<bool>(type: "bit", nullable: true),
                    isphone = table.Column<bool>(type: "bit", nullable: true),
                    adressid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customers_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_customers_adress_adressid",
                        column: x => x.adressid,
                        principalTable: "adress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    chipnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sex = table.Column<int>(type: "int", nullable: false),
                    animaltype = table.Column<int>(type: "int", nullable: false),
                    animalbreed = table.Column<int>(type: "int", nullable: false),
                    animalcolor = table.Column<int>(type: "int", nullable: false),
                    reportnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specialnote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sterilization = table.Column<bool>(type: "bit", nullable: false),
                    customersid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_patients", x => x.id);
                    table.ForeignKey(
                        name: "fk_patients_customers_customersid",
                        column: x => x.customersid,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customers_adressid",
                table: "customers",
                column: "adressid");

            migrationBuilder.CreateIndex(
                name: "ix_patients_customersid",
                table: "patients",
                column: "customersid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "adress");
        }
    }
}
