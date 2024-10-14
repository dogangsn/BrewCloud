using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vetweightcontrol",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    patientid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false),
                    controldate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("VetWeightControl_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vetweightcontrol");
        }
    }
}
