using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Accounts
{
    public class TenantDbSettings : ITenantDbSettings
    {
        public string ServerName { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
