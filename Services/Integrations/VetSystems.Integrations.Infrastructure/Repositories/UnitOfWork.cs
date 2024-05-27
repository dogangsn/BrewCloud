using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Integrations.Domain.Contracts;
using VetSystems.Integrations.Infrastructure.Persistence;
using System.Data;

namespace VetSystems.Integrations.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected VetDbContext _dbContext;
        private IDbContextTransaction _trans;
        public UnitOfWork(VetDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
            using (var db = new VetDbContext(builder.Options, null))
            {
                await db.Database.MigrateAsync();
            }
        }
        public async Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable)
        {
            var builder = new DbContextOptionsBuilder<VetDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new VetDbContext(builder.Options, null, historyTable))
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
            using (var db = new VetDbContext(builder.Options, null, ""))
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
