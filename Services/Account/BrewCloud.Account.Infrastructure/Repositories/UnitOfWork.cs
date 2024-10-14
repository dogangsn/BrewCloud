using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Infrastructure.Persistence;
using BrewCloud.Shared.Accounts;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected BrewCloudDbContext _dbContext;

        private IDbContextTransaction _trans;
        private string _errorMessage = string.Empty;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMediator _mediator;
        public UnitOfWork(BrewCloudDbContext dbContext, IIdentityRepository identityRepository, IMediator mediator)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _identityRepository = identityRepository;
            _mediator = mediator;
        }

        #region IDisposable Members
        // Burada IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public void CreateTransaction()
        {
            _trans = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _trans.Commit();
        }

        public void Rollback()
        {
            _trans.Rollback();
            _trans.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
                // sadece değişen alanları güncellemeyide sağlayabiliriz.
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.

                //foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                //throw new Exception(_errorMessage, dbEx);

                throw;
            }
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(string query, object parameters)
        {


            return _dbContext.SQLQuery<T>(query, parameters).ToList();
        }
        public List<T> QuerWithDicy<T>(string query, Dictionary<string, object> parameters)
        {
            var param = new DynamicParameters();
            foreach (var item in parameters)
            {
                param.Add(item.Key, item.Value, System.Data.DbType.String);

            }
            return _dbContext.SQLQuery<T>(query, param).ToList();
        }
        public List<T> Query<T>(string query)
        {

            return _dbContext.SQLQuery<T>(query).ToList();
        }

        public int Execute(string query, object parameters)
        {
            return _dbContext.Execute(query, parameters);
        }

        public async Task MigrateDatabase(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<BrewCloudDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new BrewCloudDbContext(builder.Options, null, _identityRepository, _mediator))
            {


                await db.Database.MigrateAsync();
            }
        }
        public async Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable)
        {
            var builder = new DbContextOptionsBuilder<BrewCloudDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new BrewCloudDbContext(builder.Options, null, _identityRepository, _mediator, historyTable))
            {
                if (string.IsNullOrEmpty(targetMigrationName))
                {
                    //db.Database.GetAppliedMigrationsAsync()
                    await db.Database.MigrateAsync();
                }
                else
                {
                    await db.MigrateAsync(targetMigrationName);
                }

            }
        }

        public async Task MoveMigrationTable(string connectionString, string historyTable)
        {
            var builder = new DbContextOptionsBuilder<BrewCloudDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new BrewCloudDbContext(builder.Options, null, _identityRepository, _mediator, ""))
            {
                var migrations = await db.Database.GetAppliedMigrationsAsync();
                foreach (var migration in migrations)
                {
                    string query =
                        $"insert into \"{historyTable}\" (migrationid,productversion) values(@migrationId,@version)";
                    db.Execute(query,
                        new
                        {
                            migrationId = migration,
                            version = "7.0.1"
                        });
                }
            }
        }
        public void ChangeDbContext(string connectionString)
        {
            _dbContext.Database.SetConnectionString(connectionString);

            // var builder = new DbContextOptionsBuilder<VeboniDbContext>();
            // builder.UseNpgsql(tenant.DatabaseConnectionString);
            //
            // _dbContext = new VeboniDbContext(builder.Options, null,_identityRepository, _mediator);
        }

    }
}
