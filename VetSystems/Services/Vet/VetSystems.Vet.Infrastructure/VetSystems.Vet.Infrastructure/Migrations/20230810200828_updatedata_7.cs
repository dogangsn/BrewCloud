using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Vet.Infrastructure.Migrations
{
    public partial class updatedata_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    unitcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unitname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Units_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "units");
        }
    }
}
