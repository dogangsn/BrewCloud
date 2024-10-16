using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Service;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Gym.Infrastructure.Persistence
{
    public class GymDbContext : DbContext
    {
        private Tenant _tenant;
        private string _historyTable;
        private readonly IIdentityRepository _identityRepository;
        public AccountInfoDto _accountInfo { get; set; }
        public GymDbContext(DbContextOptions<GymDbContext> options, ITenantRepository tenantRepository, IIdentityRepository identityRepository, string historyTable = "") : base(options)
        {
            _historyTable = historyTable;
            if (tenantRepository != null)
            {
                _tenant = tenantRepository.GetTenantById(); 
            }
            if (identityRepository != null)
                _identityRepository = identityRepository;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {

                var entites = ChangeTracker.Entries()
                                           .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                                           .Select(e => e);
                foreach (var item in entites)
                {
                    if (item.State == EntityState.Added && _accountInfo != null)
                    {

                        var propertyInfo = item.Entity.GetType().GetProperty("CreatedDate");
                        var createdbyProperty = item.Entity.GetType().GetProperty("CreateUsers");

                        var enterpricesIdProperty = item.Entity.GetType().GetProperty("EnterpricesId");
                        var enterprisesIdProperty = item.Entity.GetType().GetProperty("EnterprisesId");

                        if (enterpricesIdProperty != null && enterpricesIdProperty.CanWrite)
                        {
                            enterpricesIdProperty.SetValue(item.Entity, _accountInfo.EnterpriseId);
                        }
                        if (enterprisesIdProperty != null && enterprisesIdProperty.CanWrite)
                        {
                            enterprisesIdProperty.SetValue(item.Entity, _accountInfo.EnterpriseId);
                        }
                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            var currentDate = propertyInfo.GetValue(item.Entity);
                            //if (currentDate == null)
                            //{
                            //    propertyInfo.SetValue(item.Entity, _accountInfo.Timezone.GetSystemTimeZoneById());
                            //}
                        }
                        //if (createdbyProperty != null && createdbyProperty.CanWrite)
                        //{
                        //    createdbyProperty.SetValue(item.Entity, string.IsNullOrEmpty(_accountInfo.ContactEmail) ? _accountInfo.Email : _accountInfo.ContactEmail);
                        //}
                    }
                    if (item.State == EntityState.Modified && _accountInfo != null)
                    {
                        var propertyInfoMod = item.Entity.GetType().GetProperty("UpdateDate");
                        var modifiedbyProperty = item.Entity.GetType().GetProperty("UpdateUsers");
                        //if (propertyInfoMod != null && propertyInfoMod.CanWrite)
                        //{
                        //    propertyInfoMod.SetValue(item.Entity, _accountInfo.Timezone.GetSystemTimeZoneById());
                        //}
                        //if (modifiedbyProperty != null && modifiedbyProperty.CanWrite)
                        //{
                        //    modifiedbyProperty.SetValue(item.Entity, string.IsNullOrEmpty(_accountInfo.ContactEmail) ? _accountInfo.Email : _accountInfo.ContactEmail);
                        //}
                    }
                }
                //SaveLog(ChangeTracker.Entries());
            }
            catch (Exception ex)
            {
            }
            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
