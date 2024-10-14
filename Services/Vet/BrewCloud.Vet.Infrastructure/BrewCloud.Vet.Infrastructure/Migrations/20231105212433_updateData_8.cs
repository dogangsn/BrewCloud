using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using BrewCloud.Shared.Data;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updateData_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var service = new ConfigurationDatabaseService(Assembly.GetExecutingAssembly()))
            {

                var files = new List<string> { "UpdateData_ProductCategories", "UpdateData_Units", "UpdateData_CustomerGroupdef" };
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
