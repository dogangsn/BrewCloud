using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;

namespace VetSystems.Account.Application.Models.Accounts
{
    public class SignupRequestDto
    {
        public string CompanyId { get; set; }
        public string Email { get; set; }
        public string ContactEmail { get; set; }
        public string VknNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FirtsName { get; set; }
        public string Roleid { get; set; }
        public AccountType AccountType { get; set; }
        public string LastName { get; set; }
        public bool AuthorizeEnterprise { get; set; }
        public string TenantId { get; set; }
        public bool IsLicenceAccount { get; set; }
        public string AppKey { get; set; }
    }
}
