using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Extensions;
using VetSystems.Shared.Service;

namespace VetSystems.Integrations.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private List<Tenant> _tenantList;
        public Guid TenantId { get; set; }

        public string ConnectionDb { get; set; }

        public TenantRepository(IIdentityRepository identity, IConfiguration configuration)
        {
            TenantId = identity.TenantId;
            ConnectionDb = identity.Connection.DecryptString() + " Trust Server Certificate=true;";
        }

        public Tenant GetTenantById()
        {
            if (TenantId == Guid.Empty)
            {
                return null;
            }
            Guid tenantId = TenantId;
            string conStr = ConnectionDb;
            var tenant = new Tenant
            {
                DatabaseConnectionString = conStr
            };
            return tenant;
        }
    }
}
