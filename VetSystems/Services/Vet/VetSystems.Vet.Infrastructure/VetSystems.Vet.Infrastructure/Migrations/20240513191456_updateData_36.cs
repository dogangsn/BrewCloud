using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updateData_36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetstocktracking",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    processtype = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    piece = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    usedpiece = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    remainingpiece = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    supplierid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    expirationdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    saleprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    purchaseprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    unitid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("VetStockTracking_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetstocktracking");
        }
    }
}
