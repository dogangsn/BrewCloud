using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VetSystems.Shared.Enums;
using VetSystems.Shared.Accounts;

namespace VetSystems.IdentityServer.Infrastructure.Entities
{
    public class Accounts 
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountType AccountType { get; set; }
        public string CompanyId { get; set; }
        public string RoleId { get; set; }
        public bool? Passive { get; set; }
        public bool IsLicenceAccount { get; set; }
        public Guid TenantId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? AuthorizeEnterprise { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string VknNumber { get; set; } = string.Empty;
        public string AppKey { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
