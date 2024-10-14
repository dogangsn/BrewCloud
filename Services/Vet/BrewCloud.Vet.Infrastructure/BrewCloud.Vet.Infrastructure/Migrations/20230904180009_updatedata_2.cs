using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using BrewCloud.Shared.Data;

#nullable disable

namespace BrewCloud.Vet.Infrastructure.Migrations
{
    public partial class updatedata_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var service = new ConfigurationDatabaseService(Assembly.GetExecutingAssembly()))
            {
                //OS: 1.step (Yapıldı) Embeded file kullanıldı path olarak dosya adı veriyoruz
                //OS: 2.step (       ) Local file yerine remote file service yapılacak. Migration dan bağımsız olarak destek tarafının istediği zaman script çalıştırma yeteneği eklenecek
                var files = new List<string> { "UpdateData_AnimalBreedsDefInsert", "UpdateData_AnimalTypeDefInsert", "UpdateData_PaymentMethods" };
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
