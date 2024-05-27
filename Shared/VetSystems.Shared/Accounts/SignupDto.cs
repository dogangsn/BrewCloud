using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Enums;

namespace VetSystems.Shared.Accounts
{
    public class SignupDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountType AccountType { get; set; }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool? AuthorizeEnterprise { get; set; }
        public string RoleId { get; set; }
        public string ConnectionDb { get; set; }
        public Guid TenantId { get; set; }
        public string Host { get; set; }
        public bool IsLicenceAccount { get; set; }
        public bool UseSafeListControl { get; set; }
        public string CurrencyCode { get; set; }
        public string DefaultLanguage { get; set; }
        public string ContactEmail { get; set; }
        public string VknNumber { get; set; }
        public bool? IsComplate { get; set; }
        public string AppKey { get; set; }
        public string UserAppKey { get; set; }
        public string DashboardPath { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public bool? Active { get; set; }
        public string FirstLastName { get; set; }
        public string Phone { get; set; }
        public Guid? TitleId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public List<Guid> Accounts { get; set; }
    }
}
