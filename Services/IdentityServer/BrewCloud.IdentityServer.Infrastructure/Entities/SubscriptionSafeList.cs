using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.IdentityServer.Infrastructure.Common;

namespace BrewCloud.IdentityServer.Infrastructure.Entities
{
    public class SubscriptionSafeList : BaseEntity
    {

        public Guid EnterpriseId { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ControlType ControlType { get; set; }
        public string Address { get; set; }
        public int Action { get; set; } // Allow:True,Deny:False
    }
    public enum ControlType
    {
        IpAddress,
        Mac,
        DeviceId
    }
}
