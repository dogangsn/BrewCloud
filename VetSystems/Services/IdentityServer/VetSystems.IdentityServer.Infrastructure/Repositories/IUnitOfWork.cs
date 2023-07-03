using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace VetSystems.IdentityServer.Infrastructure.Repositories
{
        public interface IUnitOfWork: IDisposable
    {
        void CreateTransaction();
        void Commit();
        void Rollback();
        int Execute(string query, object parameters);
        List<T> Query<T>(string query, object parameters);
        List<T> Query<T>(string query);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
