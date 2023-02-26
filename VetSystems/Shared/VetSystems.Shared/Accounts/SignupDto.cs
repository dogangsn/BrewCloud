using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Shared.Accounts
{
    public class SignupDto
    {
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
        //public SubscriptionType SubscriptionType { get; set; }
        public string CurrencyCode { get; set; }
        public string DefaultLanguage { get; set; }
    }
}
