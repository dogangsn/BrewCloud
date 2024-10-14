using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.IdentityServer.Infrastructure.Services.Interface;

namespace BrewCloud.IdentityServer.Infrastructure.Services
{
    public class GrantValidator : IdentityServer4.Services.IProfileService, IExtensionGrantValidator
    {
        public string GrantType => "delegation";
        private readonly IAccountService _accountService;

        public GrantValidator(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var apikey = context.Request.Raw["appkey"];
            //var company = _accountService.GetSubscriptionApp(apikey).Result;
            //if (company != null)
            //{

            //    context.Result = new GrantValidationResult(subject: company.Id.ToString(), authenticationMethod: "delegation", claims: CreateClaims(company));
            //}
            //else
            //{
            //    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "account not found");
            //}

            return Task.FromResult(0);
        }

        private List<Claim> CreateClaims()
        {
            List<Claim> claims = new List<Claim>();

            //claims.Add(new Claim(JwtClaimTypes.IssuedAt, DateTime.Now.ToString()));
            //claims.Add(new Claim(JwtClaimTypes.Id, account.Id));
            //claims.Add(new Claim("CompanyId", account.CompanyId));
            //claims.Add(new Claim("FirstName", account.FirstName));
            //claims.Add(new Claim("LastName", account.LastName));
            //claims.Add(new Claim("AppName", account.FirstName));
            //claims.Add(new Claim("AccountType", "Test"));
            //claims.Add(new Claim("TenantId", account.TenantId.ToString()));
            //claims.Add(new Claim("ConnectionDb", account.ConnectionDb));
            //claims.Add(new Claim("Host", account.Host));

            return claims;
        }
    }
}
