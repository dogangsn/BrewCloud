using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Service;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Dapper;

namespace VetSystems.Chat.Infrastructure.Persistence
{
    public class VetDbContext : DbContext
    {
        private Tenant _tenant;
        private string _historyTable;
        public VetDbContext(DbContextOptions<VetDbContext> options, ITenantRepository tenantRepository, string historyTable = "") : base(options)
        {
            _historyTable = historyTable;
            if (tenantRepository != null)
            {
                _tenant = tenantRepository.GetTenantById();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_tenant == null)
            {
                if (!string.IsNullOrEmpty(_historyTable))
                    optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("__MigrationHistory"))
                        .UseLowerCaseNamingConvention();
                else
                {
                    optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("__MigrationHistory")).UseLowerCaseNamingConvention();
                }
            }

            if (_tenant != null)
                optionsBuilder.UseSqlServer(_tenant.DatabaseConnectionString, x => x.MigrationsHistoryTable("__MigrationHistory")).UseLowerCaseNamingConvention();

            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasPostgresExtension("uuid-ossp")
            //   .HasAnnotation("Relational:Collation", "Turkish_Turkey.1254");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
