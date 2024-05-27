using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.Shared.Accounts;

namespace VetSystems.Shared.Service
{
    public interface ITenantRepository
    {
        Tenant GetTenantById();
        string ConnectionDb { get; set; }
    }
}
