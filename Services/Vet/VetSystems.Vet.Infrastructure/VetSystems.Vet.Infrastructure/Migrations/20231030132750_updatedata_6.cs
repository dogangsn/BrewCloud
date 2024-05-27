using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedata_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remark",
                table: "vetdemandproducts");

            migrationBuilder.RenameColumn(
                name: "id1",
                table: "vetdemandproducts",
                newName: "productid");

            migrationBuilder.AddColumn<Guid>(
                name: "ownerid",
                table: "vetdemandproducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vetproductmovements",
                columns: table => new
                {
                    recid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    invoiceno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totalamount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    depotid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("VetProductMovements_pkey", x => x.recid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetproductmovements");

            migrationBuilder.DropColumn(
                name: "ownerid",
                table: "vetdemandproducts");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "vetdemandproducts",
                newName: "id1");

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "vetdemandproducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
