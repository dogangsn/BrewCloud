using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.IdentityServer.Infrastructure.Models
{
    public class AccountDomainDto
    {
        public Guid RecId { get; set; }
        public string Remark { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? PropertyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
