using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Service;

namespace BrewCloud.Farm.Infrastructure.Persistence
{
    public class FarmDbContext : DbContext
    {

        private Tenant _tenant;
        private string _historyTable;
        public FarmDbContext(DbContextOptions<FarmDbContext> options, ITenantRepository tenantRepository, string historyTable = "") : base(options)
        {
            _historyTable = historyTable;
            if (tenantRepository != null)
            {
                _tenant = tenantRepository.GetTenantById();
            }
        }

        public async Task MigrateAsync(string name)
        {
            if (Database.GetInfrastructure().GetService(typeof(IMigrator)) is IMigrator migrator) await migrator.MigrateAsync(name);
        }

        public int Execute(string query, object parameters)
        {
            return Database.GetDbConnection().Execute(query, parameters);
        }
        public List<T> SQLQuery<T>(string query, object parameters)
        {

            return Database.GetDbConnection().Query<T>(query, param: parameters).ToList();
        }
        public List<T> SQLQuery<T>(string query)
        {
            return Database.GetDbConnection().Query<T>(query).ToList();
        }

    }
}
