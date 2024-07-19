using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Infrastructure.Event;

namespace VetSystems.Vet.Infrastructure.Persistence
{
    public class VetDbContext : DbContext
    {
        private Tenant _tenant;
        private string _historyTable;
        private readonly IIdentityRepository _identityRepository;
        public AccountInfoDto _accountInfo { get; set; }
        public VetDbContext(DbContextOptions<VetDbContext> options, ITenantRepository tenantRepository, IIdentityRepository identityRepository, string historyTable = "") : base(options)
        {
            _historyTable = historyTable;
            if (tenantRepository != null)
            {
                _tenant = tenantRepository.GetTenantById();
                //_accountInfo = tenantRepository.AccountInfo;
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

        public virtual DbSet<VetProducts> VetProducts { get; set; }
        public virtual DbSet<VetLogs> VetLogs { get; set; }

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
                SaveLog(ChangeTracker.Entries());
            }
            catch (Exception ex)
            {
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        private Dictionary<string, string> relations = new Dictionary<string, string> { };

        public void SaveLog(IEnumerable<EntityEntry> entities)
        {
            List<VetLogs> logs = new List<VetLogs>();
            DateTime logDate = DateTime.Now;
            foreach (var entity in entities.Where(x => x.State == EntityState.Modified))
            {
                try
                {
                    string id = "";
                    string masterId = "";
                    var idprop = entity.CurrentValues.Properties.Where(x => x.Name == "Id" || x.Name == "Id").FirstOrDefault();
                    if (idprop == null) continue;
                    id = entity.CurrentValues[idprop].ToString();

                    if (relations.ContainsKey(entity.Entity.GetType().Name))
                    {
                        var ownerprop = entity.CurrentValues.Properties.Where(x => x.Name == relations[entity.Entity.GetType().Name]).FirstOrDefault();
                        var ownerid = entity.CurrentValues[ownerprop] == null ? "" : entity.CurrentValues[ownerprop].ToString();
                        EntityEntry topEntry = null;
                        foreach (var te in entities)
                        {
                            var recidprop = te.CurrentValues.Properties.Where(x => x.Name == "Id" || x.Name == "Id").FirstOrDefault();
                            var recid = te.CurrentValues[recidprop].ToString();
                            if (recid == ownerid)
                            {
                                topEntry = te;
                                break;
                            }
                        }

                        if (topEntry != null)
                        {

                            string topid = string.Empty;
                            bool isFoundNewTop = false;
                            while (true)
                            {
                                EntityEntry newTopEntry = null;
                                if (!relations.ContainsKey(topEntry.Entity.GetType().Name)) break;
                                var topprop = topEntry.CurrentValues.Properties.Where(x => x.Name == relations[topEntry.Entity.GetType().Name]).FirstOrDefault();
                                topid = topEntry.CurrentValues[topprop].ToString();
                                foreach (var te in entities)
                                {
                                    var recidprop = te.CurrentValues.Properties.Where(x => x.Name == "Id" || x.Name == "Id").FirstOrDefault();
                                    var recid = te.CurrentValues[recidprop].ToString();
                                    if (recid == topid)
                                    {
                                        isFoundNewTop = true;
                                        newTopEntry = te;
                                        break;
                                    }
                                }
                                if (newTopEntry == null) break;
                                if (newTopEntry.Entity == topEntry.Entity) break;
                                topEntry = newTopEntry;
                            }
                            var masterprop = topEntry.CurrentValues.Properties.Where(x => x.Name == "Id" || x.Name == "Id").FirstOrDefault();
                            masterId = topEntry.CurrentValues[masterprop].ToString();
                            if (!isFoundNewTop && !string.IsNullOrEmpty(topid))
                            {
                                masterId = topid;
                            }
                        }
                        else
                        {
                            masterId = id;
                            if (!string.IsNullOrEmpty(ownerid))
                            {
                                masterId = ownerid;

                            }
                        }
                    }
                    else
                    {
                        masterId = id;
                    }

                    foreach (var property in entity.CurrentValues.Properties)
                    {

                        if (property.Name == "CreateUsers" || property.Name == "CreateDate" 
                            || property.Name == "UpdateUsers" || property.Name == "UpdateDate"
                            || property.Name == "DeletedBy" || property.Name == "DeletedDate") continue;
                        var originalValue = entity.OriginalValues[property];
                        var updatedValue = entity.CurrentValues[property];
                        if (!Object.Equals(originalValue, updatedValue) && !((updatedValue ?? false).GetType().IsGenericType))
                        {
                            logs.Add(new VetLogs
                            {
                                Id = Guid.NewGuid(),
                                Date = logDate,
                                UserId = _identityRepository.Account.UserId,
                                UserName = _identityRepository.Account.UserName,
                                OldValue = (originalValue == null ? string.Empty : originalValue?.ToString()),
                                TableName = entity.Entity.GetType().Name,
                                FieldName = property.Name,
                                NewValue =  (updatedValue == null ? string.Empty : updatedValue?.ToString()),
                                MasterId = masterId,
                                TenantId = _identityRepository.TenantId
                            });
                        }
                    }
                }
                catch (Exception exc)
                {

                }
            }
            VetLogs.AddRangeAsync(logs);

            //_mediator.Publish(
            //    new CreateLogEvent
            //    {
            //        Logs = logs
            //    }
            //);
        }

    }
}
