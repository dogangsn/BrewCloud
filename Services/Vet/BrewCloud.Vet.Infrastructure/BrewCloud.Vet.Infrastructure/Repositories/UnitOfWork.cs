using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Infrastructure.Persistence;

namespace BrewCloud.Vet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected VetDbContext _dbContext;
        private IDbContextTransaction _trans;
        public UnitOfWork(VetDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<T> Query<T>(string query, object parameters)
        {
            return _dbContext.SQLQuery<T>(query, parameters).ToList();
        }

        public List<T> Query<T>(string query)
        {

            return _dbContext.SQLQuery<T>(query).ToList();
        }

        public int Execute(string query, object parameters)
        {
            return _dbContext.Execute(query, parameters);
        }

        public void ChangeDbContext(string connection)
        {
            //var builder = new DbContextOptionsBuilder<ErpDbContext>();
            //builder.UseNpgsql(connection);

            //_dbContext = new ErpDbContext(builder.Options, null);

            _dbContext.Database.SetConnectionString(connection);
        }

        public async Task MigrateDatabase(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<VetDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new VetDbContext(builder.Options, null, null))
            {
                await db.Database.MigrateAsync();
            }
        }

        public async Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable)
        {
            var builder = new DbContextOptionsBuilder<VetDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new VetDbContext(builder.Options, null, null, historyTable))
            {
                if (string.IsNullOrEmpty(targetMigrationName))
                {
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
            var builder = new DbContextOptionsBuilder<VetDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new VetDbContext(builder.Options, null, null, ""))
            {
                var migrations = await db.Database.GetAppliedMigrationsAsync();
                foreach (var migration in migrations)
                {
                    //string query =
                    //    $"insert into \"{historyTable}\" (migrationid,productversion) values(@migrationId,@version)";
                    //db.Execute(query,
                    //    new
                    //    {
                    //        migrationId = migration,
                    //        version = "7.0.1"
                    //    });
                }
            }
        }

        public void CreateTransaction()
        {
            // var connection = new NpgsqlConnection();
            //var tr =  connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            // _dbContext.Database.UseTransaction(tr)
            //_trans = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            _trans = _dbContext.Database.BeginTransaction();
        }

        public void CreateTransaction(IsolationLevel level, bool useTransaction = true)
        {
            if (useTransaction)
                _trans = _dbContext.Database.BeginTransaction(level);
        }

        public async Task CreateTransactionAsync(IsolationLevel level, bool useTransaction = true)
        {
            if (useTransaction)
                _trans = await _dbContext.Database.BeginTransactionAsync(level);
        }

        public void Commit(bool useTransaction = true)
        {
            if (useTransaction)
            {
                _trans.Commit();
            }
        }

        public void Rollback(bool useTransaction = true)
        {
            if (useTransaction)
            {
                _trans.Rollback();
                _trans.Dispose();
            }

        }
    }
}
