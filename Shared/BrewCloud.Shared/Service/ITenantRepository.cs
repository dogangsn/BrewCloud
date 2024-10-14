using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.Shared.Accounts;

namespace BrewCloud.Shared.Service
{
    public interface ITenantRepository
    {
        Tenant GetTenantById();
        string ConnectionDb { get; set; }
    }
}
