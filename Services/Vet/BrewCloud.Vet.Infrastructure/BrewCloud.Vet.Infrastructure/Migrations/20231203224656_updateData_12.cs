using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "doctorid",
                table: "vetappointments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "customerid",
                table: "vetappointments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "appointmenttype",
                table: "vetappointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "iscompleted",
                table: "vetappointments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "patientsid",
                table: "vetappointments",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appointmenttype",
                table: "vetappointments");

            migrationBuilder.DropColumn(
                name: "iscompleted",
                table: "vetappointments");

            migrationBuilder.DropColumn(
                name: "patientsid",
                table: "vetappointments");

            migrationBuilder.AlterColumn<string>(
                name: "doctorid",
                table: "vetappointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customerid",
                table: "vetappointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
