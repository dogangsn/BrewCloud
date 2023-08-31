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
                name: "vetpaymenttypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VetPaymentTypes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetsalebuyowner",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    customerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    invoiceno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymenttype = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    kdv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    netprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    supplierid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("VetSaleBuyOwner_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vetsalebuytrans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ownerid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ratio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    netprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    invoiceno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vetsalebuyownerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("VetSaleBuyTrans_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_vetsalebuytrans_vetsalebuyowner_vetsalebuyownerid",
                        column: x => x.vetsalebuyownerid,
                        principalTable: "vetsalebuyowner",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_vetsalebuytrans_vetsalebuyownerid",
                table: "vetsalebuytrans",
                column: "vetsalebuyownerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetpaymenttypes");

            migrationBuilder.DropTable(
                name: "vetsalebuytrans");

            migrationBuilder.DropTable(
                name: "vetsalebuyowner");
        }
    }
}
