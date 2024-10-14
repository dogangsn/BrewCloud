using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "agendanotereminder",
                table: "vetparameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "appointmentreminderduration",
                table: "vetparameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "automaticappointmentremindermessagetemplate",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "banktransfercashaccount",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "cashaccount",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "creditcardcashaccount",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "customerwelcometemplate",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "days",
                table: "vetparameters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "displayvetno",
                table: "vetparameters",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isotocustomerwelcomemessage",
                table: "vetparameters",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "smscompany",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "whatsapptemplate",
                table: "vetparameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isaccounting",
                table: "vetdemands",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isbuying",
                table: "vetdemands",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "agendanotereminder",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "appointmentreminderduration",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "automaticappointmentremindermessagetemplate",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "banktransfercashaccount",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "cashaccount",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "creditcardcashaccount",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "customerwelcometemplate",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "days",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "displayvetno",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "isotocustomerwelcomemessage",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "smscompany",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "whatsapptemplate",
                table: "vetparameters");

            migrationBuilder.DropColumn(
                name: "isaccounting",
                table: "vetdemands");

            migrationBuilder.DropColumn(
                name: "isbuying",
                table: "vetdemands");
        }
    }
}
