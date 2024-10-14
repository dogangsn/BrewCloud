using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "taxisid",
                table: "vetproducts",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "taxisid",
                table: "vetproducts");
        }
    }
}
