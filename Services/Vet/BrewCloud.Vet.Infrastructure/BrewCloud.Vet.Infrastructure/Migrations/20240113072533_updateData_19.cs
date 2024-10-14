using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetpaymentcollection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    collectionid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    debit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    credit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    paid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    totalpaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    table.PrimaryKey("VetPaymentCollection_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetpaymentcollection");
        }
    }
}
