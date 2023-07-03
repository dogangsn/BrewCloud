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
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Infrastructure.Persistence
{
    public class VetSystemsDbContext : DbContext
    {
        private Tenant _tenant;
        private readonly IMediator _mediator;
        private readonly IIdentityRepository _identityRepository;
        private string _historyTable;

        public VetSystemsDbContext(DbContextOptions<VetSystemsDbContext> options,
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

        public DbSet<Products> Products { get; set; }




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
