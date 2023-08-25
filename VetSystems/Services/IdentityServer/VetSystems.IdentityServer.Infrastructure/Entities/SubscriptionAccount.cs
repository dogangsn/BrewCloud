using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.IdentityServer.Infrastructure.Common;

namespace VetSystems.IdentityServer.Infrastructure.Entities
{
    public class SubscriptionAccount :  BaseEntity
    {

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string ActivationCode { get; set; } = "";
        public bool? IsComplate { get; set; } = false;
        public string Host { get; set; }
        public string ConnectionString { get; set; }
        public bool UseSafeListControl { get; set; }
    }
}
