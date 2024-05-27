using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Mail.Domain.Contracts;
using VetSystems.Mail.Infrastructure.Persistance;

namespace VetSystems.Mail.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected MailDbContext _dbContext;
        private IDbContextTransaction _trans;
        public UnitOfWork(MailDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region IDisposable Members
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
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
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
            _dbContext.Database.SetConnectionString(connection);
        }

        public async Task MigrateDatabase(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<MailDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new MailDbContext(builder.Options, null))
            {
                await db.Database.MigrateAsync();
            }
        }
        public async Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable)
        {
            var builder = new DbContextOptionsBuilder<MailDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new MailDbContext(builder.Options, null, historyTable))
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
            var builder = new DbContextOptionsBuilder<MailDbContext>();
            builder.UseSqlServer(connectionString);
            using (var db = new MailDbContext(builder.Options, null, ""))
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
            _trans = _dbContext.Database.BeginTransaction();
        }

        public void CreateTransaction(IsolationLevel level)
        {
            _trans = _dbContext.Database.BeginTransaction(level);


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
    }
}
