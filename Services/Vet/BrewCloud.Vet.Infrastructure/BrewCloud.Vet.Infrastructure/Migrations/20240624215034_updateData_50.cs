using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "accomodationcheckoutid",
                table: "vetaccomodation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isarchive",
                table: "vetaccomodation",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vetaccomodationcheckouts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accomodationid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    checkindate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    checkoutdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salebuyid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    accomodationamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    collectionamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paymentid = table.Column<int>(type: "int", nullable: false),
                    recid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("VetAccomodationCheckOuts_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetaccomodationcheckouts");

            migrationBuilder.DropColumn(
                name: "accomodationcheckoutid",
                table: "vetaccomodation");

            migrationBuilder.DropColumn(
                name: "isarchive",
                table: "vetaccomodation");
        }
    }
}
