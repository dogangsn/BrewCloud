using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;

namespace VetSystems.Account.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void CreateTransaction();
        void Commit();
        void Rollback();
        int Execute(string query, object parameters);
        List<T> Query<T>(string query, object parameters);
        List<T> Query<T>(string query);
        public List<T> QuerWithDicy<T>(string query, Dictionary<string, object> parameters);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task MigrateDatabase(string connectionString);
        Task MigrateDatabase(string connectionString, string targetMigrationName, string historyTable);
        Task MoveMigrationTable(string connectionString, string historyTable);
        void ChangeDbContext(string connectionString);
    }
}
