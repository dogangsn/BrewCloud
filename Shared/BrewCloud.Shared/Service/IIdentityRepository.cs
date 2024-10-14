using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.Shared.Accounts;

namespace BrewCloud.Shared.Service
{
    public interface IIdentityRepository
    {
        AccountInfoDto Account { get; }
        string Host { get; }
        Guid TenantId { get; }
        string Connection { get; set; }
        string ClientIp { get; }
        string Token { get; }
    }
}
