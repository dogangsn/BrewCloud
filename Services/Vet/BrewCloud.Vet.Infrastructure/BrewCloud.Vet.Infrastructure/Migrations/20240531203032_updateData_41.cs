using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isdefault",
                table: "vetsmstemplate",
                newName: "active");

            migrationBuilder.RenameColumn(
                name: "design",
                table: "vetsmstemplate",
                newName: "content");

            migrationBuilder.AddColumn<bool>(
                name: "enableappnotification",
                table: "vetsmstemplate",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enableemail",
                table: "vetsmstemplate",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enablesms",
                table: "vetsmstemplate",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "enablewhatsapp",
                table: "vetsmstemplate",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enableappnotification",
                table: "vetsmstemplate");

            migrationBuilder.DropColumn(
                name: "enableemail",
                table: "vetsmstemplate");

            migrationBuilder.DropColumn(
                name: "enablesms",
                table: "vetsmstemplate");

            migrationBuilder.DropColumn(
                name: "enablewhatsapp",
                table: "vetsmstemplate");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "vetsmstemplate",
                newName: "design");

            migrationBuilder.RenameColumn(
                name: "active",
                table: "vetsmstemplate",
                newName: "isdefault");
        }
    }
}
