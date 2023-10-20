using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetSystems.IdentityServer.Infrastructure.Persistence;
using VetSystems.IdentityServer.Infrastructure.Services.Interface;
using VetSystems.Shared.Accounts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VetSystems.IdentityServer.Infrastructure.Entities;
using System.Security.Principal;
using VetSystems.Shared.Dtos;
using VetSystems.IdentityServer.Infrastructure.Models;

namespace VetSystems.IdentityServer.Infrastructure.Services
{
    internal class AccountService : IAccountService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AccountService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration;
        }

        public Task<Response<bool>> CheckSafeList(string companyId, string address)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> CreateAccountDomain(AccountDomainDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> CreateSafeList(SafeListDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteAccounDomain(string recId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteSafeList(string recId)
        {
            throw new NotImplementedException();
        }

        public async Task<Accounts> GetAccountByEmail(string email)
        {
            var result = await _dbContext.Accounts.FirstOrDefaultAsync(r => r.User.Email == email);
            return result;
        }

        public Task<Accounts> GetAccountById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<SignupDto> GetAccountByIdForClaims(string id)
        {
            var result = await (from ac in _dbContext.Accounts
                                join sb in _dbContext.SubscriptionAccounts on ac.TenantId equals sb.Recid
                                where ac.UserId == id
                                select new SignupDto
                                {
                                    Email = ac.User.Email,
                                    Id = ac.UserId,
                                    IsLicenceAccount = ac.IsLicenceAccount,
                                    CompanyId = ac.CompanyId,
                                    RoleId = ac.RoleId,
                                    TenantId = ac.TenantId,
                                    AuthorizeEnterprise = ac.AuthorizeEnterprise,
                                    FirstName = ac.FirstName,
                                    LastName = ac.LastName,
                                    Host = sb.Host ?? "",
                                    AccountType = ac.AccountType,
                                    ConnectionDb = sb.ConnectionString,
                                    UseSafeListControl = sb.UseSafeListControl,
                                    UserName = ac.User.UserName,
                                    //SubscriptionType = sb.SubscriptionType
                                }).FirstOrDefaultAsync();
            if (result != null)
            {
            }
            return result;
        }

        public Task<Response<List<AccountDomainDto>>> GetAccountDomains(string companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SignupDto>> GetCompanyUsersAsync(string companyId)
        {
            var result = await (from ac in _dbContext.Accounts
                               join sb in _dbContext.SubscriptionAccounts on ac.TenantId equals sb.Recid
                               //join app in _dbContext.SubscriptionApps on new { ac.UserId, Deleted = false } equals new { app.UserId, app.Deleted } into sapp
                               //from userApp in sapp.DefaultIfEmpty()
                               where ac.CompanyId == companyId
                               select new SignupDto
                               {
                                   Email = ac.User.Email,
                                   UserName = ac.User.UserName,
                                   FirstName = ac.FirstName,
                                   LastName = ac.LastName,
                                   Id = ac.UserId,
                                   AccountType = ac.AccountType,
                                   CompanyId = ac.CompanyId,
                                   RoleId = ac.RoleId,
                                   AuthorizeEnterprise = ac.AuthorizeEnterprise,
                                   TenantId = ac.TenantId,
                                   ConnectionDb = sb.ConnectionString,
                                   IsLicenceAccount = ac.IsLicenceAccount,
                                   UseSafeListControl = sb.UseSafeListControl,
                                   ContactEmail = ac.ContactEmail ?? "",
                                   VknNumber = ac.VknNumber ?? "",
                                   //SubscriptionType = sb.SubscriptionType,
                                   //UserAppKey = userApp.AppKey,
                                   AppKey = ac.AppKey
                               }).ToListAsync();
            return result;
        }

        public Task<Response<List<SafeListDto>>> GetSafeList(string companyId)
        {
            throw new NotImplementedException();
        }

        public Task<SignupDto> GetSubscriptionApp(string appKey)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateAccountDomain(AccountDomainDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateSafeList(SafeListDto model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateTenantAsync(string recId, bool useSafeList)
        {
            throw new NotImplementedException();
        }
    }
}
