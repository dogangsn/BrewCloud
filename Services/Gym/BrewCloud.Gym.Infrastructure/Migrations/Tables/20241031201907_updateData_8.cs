using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Gym.Infrastructure.Migrations.Tables
{
    public partial class updateData_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gympersonnel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    identitynumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    graduate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branchid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("GymPersonnel_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gympersonnelpermission",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isapproved = table.Column<bool>(type: "bit", nullable: false),
                    personnelid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    permissiontype = table.Column<int>(type: "int", nullable: false),
                    begindate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    enddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("GymPersonnelPermission_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gympersonnel");

            migrationBuilder.DropTable(
                name: "gympersonnelpermission");
        }
    }
}
