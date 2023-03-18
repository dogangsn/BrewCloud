using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
