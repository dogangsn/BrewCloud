using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetlogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    oldvalue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    newvalue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fieldname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenantid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tablename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    masterid = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("VetLogs_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetlogs");
        }
    }
}
