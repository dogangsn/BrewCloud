using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.IdentityServer.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CreateTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int Execute(string query, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(string query, object parameters)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>(string query)
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
