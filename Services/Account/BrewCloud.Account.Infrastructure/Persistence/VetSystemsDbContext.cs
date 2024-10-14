using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Infrastructure.Persistence
{
    public class BrewCloudDbContext : DbContext
    {
        private Tenant _tenant;
        private readonly IMediator _mediator;
        private readonly IIdentityRepository _identityRepository;
        private string _historyTable;

        public BrewCloudDbContext(DbContextOptions<BrewCloudDbContext> options,
               ITenantRepository tenantRepository,
               IIdentityRepository identityRepository, IMediator mediator, string historyTable = "") : base(options)
        {
            if (tenantRepository != null)
            {
                _tenant = tenantRepository.GetTenantById();
            }

            _identityRepository = identityRepository;
            _mediator = mediator;
            _historyTable = historyTable;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Enterprise> Enterprise { get; set; }
        public virtual DbSet<RoleSettingDetail> RoleSettingDetail { get; set; }
        public virtual DbSet<TempAccount> TempAccount { get; set; }
        public virtual DbSet<Reason> Reason { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Abilitygroup> Abilitygroup { get; set; }
        public virtual DbSet<Rolesetting> Rolesetting { get; set; }
        public virtual DbSet<ReasonProperties> ReasonProperties { get; set; }
        public virtual DbSet<Userauthorization> Userauthorization { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<TitleDefinitions> TitleDefinitions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_tenant == null)
            {
                if (!string.IsNullOrEmpty(_historyTable))
                    optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("__AccountMigrationHistory"))
                        .UseLowerCaseNamingConvention();
                else
                {
                    optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("__AccountMigrationHistory")).UseLowerCaseNamingConvention();
                }
            }


            if (_tenant != null)
                optionsBuilder
                    .UseSqlServer(_tenant.DatabaseConnectionString, x => x.MigrationsHistoryTable("__AccountMigrationHistory")).UseLowerCaseNamingConvention();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            base.OnConfiguring(optionsBuilder);
        }


        public Tenant GetTenant()
        {
            return _tenant;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var modifiedAuditedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified)
                    .Select(s => new { s.Entity, s.OriginalValues, s.CurrentValues });
                foreach (var item in modifiedAuditedEntities)
                {
                    //SaveLog(item.Entity, item.OriginalValues.ToObject(), item.CurrentValues.ToObject());
                }
            }
            catch (Exception)
            {
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
  
        }

        public async Task MigrateAsync(string name)
        {
            if (Database.GetInfrastructure().GetService(typeof(IMigrator)) is IMigrator migrator)
                await migrator.MigrateAsync(name);
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
