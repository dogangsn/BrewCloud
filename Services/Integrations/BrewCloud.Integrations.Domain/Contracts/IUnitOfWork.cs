using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Integrations.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        int Execute(string query, object parameters);
        List<T> Query<T>(string query, object parameters);
        List<T> Query<T>(string query);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task MigrateDatabase(string connectionString);
        Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable);
        Task MoveMigrationTable(string connectionString, string historyTable);

        void ChangeDbContext(string connection);
        void CreateTransaction(IsolationLevel level, bool useTransaction = true);

        void Commit(bool useTransaction = true);
        void Rollback(bool useTransaction = true);

    }
}
