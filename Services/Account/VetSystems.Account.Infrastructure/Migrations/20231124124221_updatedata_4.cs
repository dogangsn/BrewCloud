using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using VetSystems.Shared.Data;

#nullable disable

namespace VetSystems.Account.Infrastructure.Migrations
{
    public partial class updatedata_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var service = new ConfigurationDatabaseService(Assembly.GetExecutingAssembly()))
            {

                var files = new List<string> { "UpdateData_TitleDefination"  };
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
