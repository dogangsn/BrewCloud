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
        Task MigrateDatabase(Tenant _tenant);
    }
}
