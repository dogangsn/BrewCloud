using Identity.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Application.Models.Accounts;
using VetSystems.Shared.Accounts;

namespace VetSystems.Account.Application.GrpServices
{
    public class IdentityGrpService
    {
        private readonly IdentityUserProtoService.IdentityUserProtoServiceClient _identityProtoService;

        public IdentityGrpService(IdentityUserProtoService.IdentityUserProtoServiceClient identityProtoService)
        {
            _identityProtoService = identityProtoService ?? throw new ArgumentNullException(nameof(identityProtoService));
        }

        public async Task<IdentityResponse> CreateUserAsync(string username,
                                                            string password,
                                                            string email,
                                                            string companyId,
                                                            string firstname,
                                                            string lastname,
                                                            string roleid,
                                                            string tenantId,
                                                            SignupRequest.Types.AccountType accountType, bool isLicenceAccount)
        {
            var request = new SignupRequest
            {
                CompanyId = companyId,
                Email = email,
                Password = password,
                UserName = username,
                FirtsName = firstname,
                Roleid = roleid,
                AccountType = accountType,
                LastName = lastname,
                TenantId = tenantId,
                IsLicenceAccount = isLicenceAccount
            };
            return await _identityProtoService.CreateUserAsync(request);
        }

        public async Task<CompanyUsersResponse> GetCompanyUsersAsync(string enterpriseId)
        {
            var request = new UserRequest
            {
                CompanyId = enterpriseId
            };
            var users = await _identityProtoService.GetUsersByCompanyAsync(request);
            return users;
        }

        public async Task<UserResponse> GetUserByIdAsync(string userId)
        {
            var request = new UserRequest
            {
                UserId = userId
            };
            var users = await _identityProtoService.GetUserByIdAsync(request);
            return users;
        }


        public async Task<UserResponse> GetUserByEmailAsync(string email)
        {
            var request = new UserRequest
            {
                Email = email
            };
            var users = await _identityProtoService.GetUserByEmailAsync(request);
            return users;
        }

        public async Task<IdentityResponse> DeleteUserByIdAsync(string userId)
        {
            var request = new DeleteRequest
            {
                Id = userId
            };
            var response = await _identityProtoService.DeleteUserAsync(request);
            return response;
        }

        private SignupRequest.Types.AccountType ConvertAccountType(AccountType accountType)
        {
            SignupRequest.Types.AccountType _accountType = SignupRequest.Types.AccountType.User;
            switch (accountType)
            {
                case AccountType.None:
                    _accountType = SignupRequest.Types.AccountType.User;
                    break;
                case AccountType.CompanyAdmin:
                    _accountType = SignupRequest.Types.AccountType.Companyadmin;
                    break;
                case AccountType.User:
                    _accountType = SignupRequest.Types.AccountType.User;
                    break;
                case AccountType.Admin:
                    _accountType = SignupRequest.Types.AccountType.Admin;
                    break;
                case AccountType.B2BSale:
                    _accountType = SignupRequest.Types.AccountType.B2Bsale;
                    break;
                case AccountType.B2BAgency:
                    _accountType = SignupRequest.Types.AccountType.B2Bagency;
                    break;
                case AccountType.B2BPerson:
                    _accountType = SignupRequest.Types.AccountType.B2Bperson;
                    break;
                case AccountType.B2BOperator:
                    _accountType = SignupRequest.Types.AccountType.B2Boperator;
                    break;
                case AccountType.B2BHotel:
                    _accountType = SignupRequest.Types.AccountType.B2Bhotel;
                    break;
                case AccountType.B2BCustomer:
                    _accountType = SignupRequest.Types.AccountType.B2Bcustomer;
                    break;
                default:
                    _accountType = SignupRequest.Types.AccountType.User;
                    break;
            }

            return _accountType;
        }
        public async Task<IdentityResponse> RegisterUserAsync(SignupRequestDto signupRequest)
        {
            var request = new SignupRequest
            {
                CompanyId = signupRequest.CompanyId,
                Email = signupRequest.Email,
                Password = signupRequest.Password,
                UserName = signupRequest.UserName,
                FirtsName = signupRequest.FirtsName,
                Roleid = signupRequest.Roleid,
                AccountType = SignupRequest.Types.AccountType.User,//ConvertAccountType(signupRequest.AccountType),
                LastName = signupRequest.LastName,
                AuthorizeEnterprise = signupRequest.AuthorizeEnterprise,
                TenantId = signupRequest.TenantId,
                IsLicenceAccount = signupRequest.IsLicenceAccount,
                //ContactEmail = signupRequest.ContactEmail,
                //VknNumber = signupRequest.VknNumber,
                AppKey = signupRequest.AppKey
            };
            return await _identityProtoService.RegisterUserAsync(request);
        }

        public async Task<IdentityResponse> UpdateUserAsync(string username,
                                                         string password,
                                                         string email,
                                                         string companyId,
                                                         string firstname,
                                                         string lastname,
                                                         string roleid,
                                                         bool authorizeEnterprise, bool isLicenceAccount, string vknNumber, string appKey)
        {
            var request = new SignupRequest
            {
                CompanyId = companyId,
                Email = email,
                Password = password,
                UserName = username,
                FirtsName = firstname,
                Roleid = roleid,
                AccountType = SignupRequest.Types.AccountType.User,
                LastName = lastname,
                AuthorizeEnterprise = authorizeEnterprise,
                IsLicenceAccount = isLicenceAccount,
                VknNumber = vknNumber,
                AppKey = appKey

            };
            return await _identityProtoService.UpdateUserAsync(request);
        }



        public async Task<IdentityResponse> CreateSafeList(SafeListRequest request)
        {
            return await _identityProtoService.CreateSafeListAsync(request);
        }

        public async Task<IdentityResponse> UpdateSafeList(SafeListRequest request)
        {
            return await _identityProtoService.UpdateSafeListAsync(request);
        }

        public async Task<IdentityResponse> DeleteSafeList(DeleteRequest request)
        {
            return await _identityProtoService.DeleteSafeListAsync(request);
        }

        public async Task<SafeListCompanyResponse> GetSafeList(SafeListControlRequest request)
        {
            return await _identityProtoService.GetSafeListAsync(request);
        }
        public async Task<IdentityResponse> UpdateTenantAsync(TenantUpdateRequest request)
        {
            return await _identityProtoService.UpdateTenantAsync(request);
        }

        public async Task<IdentityResponse> CheckSafeListAsync(SafeListControlRequest request)
        {
            return await _identityProtoService.CheckSafeListAsync(request);
        }

        public async Task<IdentityResponse> CreateAccountDomainAsync(AccountDomainModel request)
        {
            return await _identityProtoService.CreateAccountDomainAsync(request);
        }
        public async Task<IdentityResponse> DeleteAccountDomainAsync(AccountDomainModel request)
        {
            return await _identityProtoService.DeleteAccountDomainAsync(request);
        }

        public async Task<IdentityResponse> UpdateAccountDomainAsync(AccountDomainModel request)
        {
            return await _identityProtoService.UpdateAccountDomainAsync(request);
        }

        public async Task<AccountDomainResponse> GetAccountDomainsAsync(AccountDomainModel request)
        {
            return await _identityProtoService.GetAccountDomainsAsync(request);
        }

    }
}
