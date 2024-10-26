using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Gym.Infrastructure.Migrations
{
    public partial class updateData_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gymmember",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    identitynumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ismember = table.Column<bool>(type: "bit", nullable: false),
                    ismaried = table.Column<bool>(type: "bit", nullable: false),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<byte>(type: "tinyint", nullable: false),
                    blooptype = table.Column<byte>(type: "tinyint", nullable: false),
                    emergencyperson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emergencypersonphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    personneltrainerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("GymMember_pkey", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gymmember");
        }
    }
}
