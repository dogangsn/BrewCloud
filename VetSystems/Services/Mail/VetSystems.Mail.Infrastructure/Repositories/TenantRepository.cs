using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Extensions;
using VetSystems.Shared.Service;

namespace VetSystems.Mail.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private List<Tenant> _tenantList;
        public Guid TenantId { get; set; }

        public string ConnectionDb { get; set; }
        public TenantRepository(IIdentityRepository identity, IConfiguration configuration)
        {
            TenantId = identity.TenantId;
            ConnectionDb = identity.Connection.DecryptString();
            _tenantList = new List<Tenant>
            {
                new Tenant
                {
                    Name ="Customer A",
                    Id =Guid.Parse("d264cd96-e374-4c1e-b493-62ff741be385"),
                    DatabaseConnectionString="Server=192.168.40.15;Port=5432;Database=veboni-51C2KJQIA6;User Id=postgres;Password=Xidok4096H;"
                },
                new Tenant
                {
                    Name ="Customer B",
                    Id =Guid.Parse("913da071-2721-452d-910b-77276e60485d"),
                    DatabaseConnectionString="Server=192.168.40.15;Port=5432;Database=veboni-PV3M5GKV15;User Id=postgres;Password=Xidok4096H;"
                },
                new Tenant
                {
                    Name ="Customer C",
                    Id =Guid.Parse("0fd8927c-8ffb-4e5b-94a8-79f953d477e2"),
                    DatabaseConnectionString="Server=192.168.40.15;Port=5432;Database=veboni-SE3KEEPIXV;User Id=postgres;Password=Xidok4096H;"
                },
            };
        }

        public TenantRepository()
        {
            _tenantList = new List<Tenant>
            {
                new Tenant
                {
                    Name ="Customer A",
                    Id =Guid.Parse("d264cd96-e374-4c1e-b493-62ff741be385"),
                    DatabaseConnectionString="Server=192.168.40.15;Port=5432;Database=veboni-51C2KJQIA6;User Id=postgres;Password=Xidok4096H;"
                },
                new Tenant
                {
                    Name ="Customer B",
                    Id =Guid.Parse("913da071-2721-452d-910b-77276e60485d"),
                    DatabaseConnectionString="Server=192.168.40.15;Port=5432;Database=veboni-PV3M5GKV15;User Id=postgres;Password=Xidok4096H;"
                },
                new Tenant
                {
                    Name ="Customer C",
                    Id =Guid.Parse("0fd8927c-8ffb-4e5b-94a8-79f953d477e2"),
                    DatabaseConnectionString="Server=192.168.40.15;Port=5432;Database=veboni-SE3KEEPIXV;User Id=postgres;Password=Xidok4096H;"
                },
            };
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
