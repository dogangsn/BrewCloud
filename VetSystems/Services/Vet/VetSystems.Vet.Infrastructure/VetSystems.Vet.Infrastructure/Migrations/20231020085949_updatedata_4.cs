using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedata_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetdemandproducts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    unitprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    stockstate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    isactive = table.Column<int>(type: "int", nullable: true),
                    reserved = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("VetDemandProducts_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetdemandproducts");
        }
    }
}
