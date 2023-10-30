using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Service;

namespace VetSystems.Farm.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        public string ConnectionDb { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Tenant GetTenantById()
        {
            throw new NotImplementedException();
        }
    }
}
