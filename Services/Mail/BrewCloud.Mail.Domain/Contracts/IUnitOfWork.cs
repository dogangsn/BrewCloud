using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Mail.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        int Execute(string query, object parameters);
        List<T> Query<T>(string query, object parameters);
        List<T> Query<T>(string query);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void ChangeDbContext(string connection);

        Task MigrateDatabase(string connectionString);
        Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable);
        Task MoveMigrationTable(string connectionString, string historyTable);

        void CreateTransaction(IsolationLevel level);
        void Commit();
        void Rollback();
    }
}
