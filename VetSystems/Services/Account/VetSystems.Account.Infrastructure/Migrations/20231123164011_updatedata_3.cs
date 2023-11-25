using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetSystems.Account.Infrastructure.Migrations
{
    public partial class updatedata_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "title",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "titledefinitions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createuser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateuser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_titledefinitions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "titledefinitions");

            migrationBuilder.DropColumn(
                name: "title",
                table: "users");
        }
    }
}
