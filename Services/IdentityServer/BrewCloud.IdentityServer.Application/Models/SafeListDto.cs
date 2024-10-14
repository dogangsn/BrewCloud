using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.IdentityServer.Infrastructure.Entities;

namespace BrewCloud.IdentityServer.Application.Models
{
    public class SafeListDto
    {
        public Guid RecId { get; set; }
        public Guid Enterpriseıd { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ControlType ControlType { get; set; }
        public int Action { get; set; }
        public string Address { get; set; }
    }
}
