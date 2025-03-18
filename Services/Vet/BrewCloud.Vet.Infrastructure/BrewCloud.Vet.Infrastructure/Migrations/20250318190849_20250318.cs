using BrewCloud.Shared.Data;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class _20250318 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var service = new ConfigurationDatabaseService(Assembly.GetExecutingAssembly()))
            {
                var files = new List<string> { "UpdateData_AppointmentTypes" };
                foreach (var item in files)
                {
                    var query = service.GetSqlText(item);
                    if (!string.IsNullOrEmpty(query))
                    {
                        migrationBuilder.Sql(query);
                    }
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
