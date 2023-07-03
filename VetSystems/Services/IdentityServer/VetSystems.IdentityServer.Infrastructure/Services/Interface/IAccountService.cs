using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Models;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;

namespace VetSystems.IdentityServer.Infrastructure.Services.Interface
{
    public interface IAccountService
    {
        Task<Accounts> GetAccountByEmail(string email);
        Task<SignupDto> GetAccountByIdForClaims(string id);

        Task<Accounts> GetAccountById(string id);
        Task<List<SignupDto>> GetCompanyUsersAsync(string companyId);

        Task<SignupDto> GetSubscriptionApp(string appKey);

        Task<Response<bool>> CheckSafeList(string companyId, string address);

        Task<Response<bool>> CreateSafeList(SafeListDto model);

        Task<Response<bool>> UpdateSafeList(SafeListDto model);

        Task<Response<bool>> DeleteSafeList(string recId);

        Task<Response<List<SafeListDto>>> GetSafeList(string companyId);

        Task<Response<bool>> UpdateTenantAsync(string recId, bool useSafeList);
        Task<Response<List<AccountDomainDto>>> GetAccountDomains(string companyId);
        Task<Response<bool>> CreateAccountDomain(AccountDomainDto model);
        Task<Response<bool>> UpdateAccountDomain(AccountDomainDto model);
        Task<Response<bool>> DeleteAccounDomain(string recId);
    }
}
