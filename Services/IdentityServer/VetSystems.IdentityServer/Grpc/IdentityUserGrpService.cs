using Identity.Api;
using Microsoft.AspNetCore.Identity;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Grpc.Core;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Models;

namespace VetSystems.IdentityServer.Grpc
{
    public class IdentityUserGrpService : IdentityUserProtoService.IdentityUserProtoServiceBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;

        public IdentityUserGrpService(UserManager<ApplicationUser> userManager, IAccountService accountService, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public override async Task<IdentityResponse> CreateUser(SignupRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.UserName,
                Account = new Accounts
                {
                    CompanyId = request.CompanyId,
                    AccountType = (AccountType)((int)request.AccountType),
                    FirstName = request.FirtsName,
                    LastName = request.LastName,
                    Passive = request.Passive,
                    RoleId = request.Roleid,
                    TenantId = !string.IsNullOrEmpty(request.TenantId) ? Guid.Parse(request.TenantId) : Guid.Empty,
                    AuthorizeEnterprise = request.AuthorizeEnterprise,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "",
                    IsLicenceAccount = request.IsLicenceAccount

                }
            };

            context.Status = new Status(StatusCode.OK, string.Empty);
            var result = await _userManager.CreateAsync(user, request.Password);
            identityResponse.IsSuccess = result.Succeeded;
            if (!result.Succeeded)
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', result.Errors.Select(x => x.Description).ToArray()));
                identityResponse.Message = context.Status.Detail;
            }


            identityResponse.Id = user.Id;
            return identityResponse;

        }

        public override async Task<IdentityResponse> DeleteUser(DeleteRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                context.Status = new Status(StatusCode.NotFound, $"user not found");
                return identityResponse;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', result.Errors.Select(x => x.Description).ToArray()));
                identityResponse.Message = context.Status.Detail;
                return identityResponse;
            }

            context.Status = new Status(StatusCode.OK, string.Empty);

            return identityResponse;
        }

        public override async Task<UserResponse> GetUserById(UserRequest request, ServerCallContext context)
        {
            var userResponse = new UserResponse();

            var user = await _userManager.FindByIdAsync(request.UserId);
            user.Account = await _accountService.GetAccountById(user.Id);

            if (user == null)
            {
                context.Status = new Status(StatusCode.NotFound, $"user not found");
                return userResponse;
            }
            context.Status = new Status(StatusCode.OK, string.Empty);
            userResponse.CompanyId = user.Account.CompanyId;
            userResponse.FirstName = user.Account.FirstName;
            userResponse.LastName = user.Account.LastName;
            userResponse.Id = user.Id;
            userResponse.AuthorizeEnterprise = user.Account.AuthorizeEnterprise.GetValueOrDefault();
            userResponse.Roleid = user.Account.RoleId;
            userResponse.AccountType = (int)user.Account.AccountType;
            userResponse.IsLicenceAccount = user.Account.IsLicenceAccount;


            return userResponse;
        }

        public override async Task<CompanyUsersResponse> GetUsersByCompany(UserRequest request, ServerCallContext context)
        {
            var response = new CompanyUsersResponse();
            var users = await _accountService.GetCompanyUsersAsync(request.CompanyId);// await _userManager.Users.Where(r => r.Account.CompanyId == request.CompanyId).ToListAsync();
            context.Status = !users.Any() ? new Status(StatusCode.NotFound, $"user not found") : context.Status = new Status(StatusCode.OK, string.Empty);
            response.IsSuccess = users.Any();
            response.Message = context.Status.Detail;

            users.ForEach(r =>
            {
                response.Data.Add(new UserResponse
                {
                    AccountType = (int)r.AccountType,
                    CompanyId = r.CompanyId,
                    FirstName = r.FirstName,
                    Email = r.Email,
                    Roleid = r.RoleId ?? "",
                    // AuthorizeEnterprise = r.AuthorizeEnterprise.GetValueOrDefault(),
                    Id = r.Id,
                    LastName = r.LastName,
                    IsLicenceAccount = r.IsLicenceAccount,
                    ContactEmail = r.ContactEmail,
                    VknNumber = r.VknNumber,
                    AppKey = r.AppKey ?? "",
                    UserAppKey = r.UserAppKey ?? ""
                });
            });




            return response;
        }


        public override async Task<UserResponse> GetUserByEmail(UserRequest request, ServerCallContext context)
        {
            var userResponse = new UserResponse();

            var user = await _accountService.GetAccountByEmail(request.Email);

            if (user == null)
            {
                context.Status = new Status(StatusCode.NotFound, $"user not found");
                return userResponse;
            }
            context.Status = new Status(StatusCode.OK, string.Empty);
            userResponse.CompanyId = user.CompanyId;
            userResponse.FirstName = user.FirstName;
            userResponse.LastName = user.LastName;
            userResponse.Id = user.UserId;
            userResponse.AccountType = (int)user.AccountType;
            userResponse.IsLicenceAccount = user.IsLicenceAccount;

            return userResponse;
        }

        private AccountType ConvertAccountType(SignupRequest.Types.AccountType accountType)
        {
            AccountType _accountType = AccountType.User;
            switch (accountType)
            {
                case SignupRequest.Types.AccountType.None:
                    _accountType = AccountType.None;
                    break;
                case SignupRequest.Types.AccountType.Companyadmin:
                    _accountType = AccountType.CompanyAdmin;
                    break;
                case SignupRequest.Types.AccountType.User:
                    _accountType = AccountType.User;
                    break;
                case SignupRequest.Types.AccountType.Admin:
                    _accountType = AccountType.Admin;
                    break;
                case SignupRequest.Types.AccountType.B2Bsale:
                    _accountType = AccountType.B2BSale;
                    break;
                case SignupRequest.Types.AccountType.B2Bagency:
                    _accountType = AccountType.B2BAgency;
                    break;
                case SignupRequest.Types.AccountType.B2Bperson:
                    _accountType = AccountType.B2BPerson;
                    break;
                case SignupRequest.Types.AccountType.B2Boperator:
                    _accountType = AccountType.B2BOperator;
                    break;
                case SignupRequest.Types.AccountType.B2Bhotel:
                    _accountType = AccountType.B2BHotel;
                    break;
                case SignupRequest.Types.AccountType.B2Bcustomer:
                    _accountType = AccountType.B2BCustomer;
                    break;
                default:
                    _accountType = AccountType.User;
                    break;
            }

            return _accountType;
        }
        public override async Task<IdentityResponse> RegisterUser(SignupRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var checkUser = await _userManager.FindByEmailAsync(request.Email.Trim());
            if (checkUser != null && !String.IsNullOrEmpty(checkUser.Email))
            {

                identityResponse.Message = "E-mail address is already taken";
                return identityResponse;

            }


            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,

                Account = new Accounts
                {
                    CompanyId = request.CompanyId,//Guid.NewGuid().ToString(),
                    AccountType = ConvertAccountType(request.AccountType),
                    FirstName = request.FirtsName,
                    LastName = request.LastName,
                    AuthorizeEnterprise = request.AuthorizeEnterprise,
                    RoleId = request.Roleid,
                    TenantId = !string.IsNullOrEmpty(request.TenantId) ? Guid.Parse(request.TenantId) : Guid.Empty,
                    Passive = false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "",
                    ContactEmail = request.ContactEmail,
                    VknNumber = request.VknNumber,
                    IsLicenceAccount = request.IsLicenceAccount,
                    AppKey = request.AppKey,
                    UserName = request.Email,
                }

            };


            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                identityResponse.IsSuccess = result.Succeeded;

                //if (request.AccountType != SignupRequest.Types.AccountType.User)
                //{
                //    await SendB2bMail(user);
                //}
                //else
                //{
                //    await SendMail(user);
                //}

                identityResponse.Id = user.Id;

            }
            else
            {
                //context.Status = new Status(StatusCode.NotFound, string.Join(',', result.Errors.Select(x => x.Description).ToArray()));
                identityResponse.Message = string.Join(',', result.Errors.Select(x => x.Description).ToArray());// context.Status.Detail;
            }

            return identityResponse;
        }

        //private async Task SendB2bMail(ApplicationUser user)
        //{
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //    string codeHtmlVersion = code;// WebUtility.UrlEncode(code);// code.EncryptStringAsUrl(); //WebUtility.UrlEncode(code);
        //    string baseUrl = _configuration["VeboniSPAUrl"];
        //    string callbackUrl = HtmlEncoder.Default.Encode($"{baseUrl}/auth/reset-password/{user.Id}/{codeHtmlVersion}/1");

        //    var eventMessage = new SendMailEvent
        //    {
        //        To = user.Account.ContactEmail,
        //        Subject = "Account Confirmation.",
        //        Body = $"Username is <b>{user.Email}</b> <br> Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\" target=\"_blank\">link</a>"
        //    };


        //    await _publishEndpoint.Publish<SendMailEvent>(eventMessage);

        //}
        //private async Task SendMail(ApplicationUser user)
        //{
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //    string codeHtmlVersion = code;// WebUtility.UrlEncode(code);// code.EncryptStringAsUrl(); //WebUtility.UrlEncode(code);
        //    string baseUrl = _configuration["VeboniSPAUrl"];
        //    string callbackUrl = HtmlEncoder.Default.Encode($"{baseUrl}/auth/reset-password/{user.Id}/{codeHtmlVersion}/1");

        //    var eventMessage = new SendMailEvent
        //    {
        //        To = user.Email,
        //        Subject = "Account Confirmation.",
        //        Body = "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\" target=\"_blank\">link</a>"
        //    };


        //    await _publishEndpoint.Publish<SendMailEvent>(eventMessage);

        //}
        public override async Task<IdentityResponse> UpdateUser(SignupRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var user = await _userManager.FindByEmailAsync(request.Email.Trim());
            if (user == null)
            {

                identityResponse.Message = "user not found";
                return identityResponse;

            }

            user.Account = await _accountService.GetAccountById(user.Id);
            user.Account.FirstName = request.FirtsName;
            user.Account.LastName = request.LastName;
            //  user.Account.AccountType = (AccountType)signupDto.AccountType;
            user.Account.AuthorizeEnterprise = request.AuthorizeEnterprise;
            user.Account.RoleId = request.Roleid;
            user.Account.ModifiedBy = "";//_identityRepository.Account.UserId.ToString();
            user.Account.ModifiedDate = DateTime.Now;
            user.Account.IsLicenceAccount = request.IsLicenceAccount;
            user.Account.VknNumber = request.VknNumber;
            user.Account.AppKey = request.AppKey;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                identityResponse.IsSuccess = result.Succeeded;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', result.Errors.Select(x => x.Description).ToArray()));
                identityResponse.Message = context.Status.Detail;
            }

            return identityResponse;
        }

        public override async Task<IdentityResponse> UpdateSafeList(SafeListRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();
            var model = new SafeListDto
            {
                RecId = new Guid(request.Id),
                Action = request.Action,
                Address = request.Address,
                Description = request.Description,
                Enterpriseıd = new Guid(request.EnterpriseId),
                TenantId = new Guid(request.TenantId),
                Name = request.Name,
            };

            if (request.ControlType == SafeListRequest.Types.ControlType.Ip)
            {
                model.ControlType = ControlType.IpAddress;
            }
            if (request.ControlType == SafeListRequest.Types.ControlType.Mac)
            {
                model.ControlType = ControlType.Mac;
            }
            if (request.ControlType == SafeListRequest.Types.ControlType.Other)
            {
                model.ControlType = ControlType.DeviceId;
            }
            var response = await _accountService.UpdateSafeList(model);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }

            return identityResponse;
        }

        public override async Task<IdentityResponse> CreateSafeList(SafeListRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();
            var model = new SafeListDto
            {
                Action = request.Action,
                Address = request.Address,
                Description = request.Description,
                Enterpriseıd = new Guid(request.EnterpriseId),
                TenantId = new Guid(request.TenantId),
                Name = request.Name,
            };

            if (request.ControlType == SafeListRequest.Types.ControlType.Ip)
            {
                model.ControlType = ControlType.IpAddress;
            }
            if (request.ControlType == SafeListRequest.Types.ControlType.Mac)
            {
                model.ControlType = ControlType.Mac;
            }
            if (request.ControlType == SafeListRequest.Types.ControlType.Other)
            {
                model.ControlType = ControlType.DeviceId;
            }
            var response = await _accountService.CreateSafeList(model);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }

            return identityResponse;
        }

        public override async Task<IdentityResponse> DeleteSafeList(DeleteRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var response = await _accountService.DeleteSafeList(request.Id);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }

            return identityResponse;
        }

        public override async Task<IdentityResponse> CheckSafeList(SafeListControlRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var control = await _accountService.CheckSafeList(request.EnterpriseId, request.Address);
            if (control != null && control.ResponseType == ResponseType.Error)
            {
                identityResponse.IsSuccess = false;
                //context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { control.Message }));
                identityResponse.Message = control.Data.ToString();// context.Status.Detail;
            }
            else
            {
                identityResponse.IsSuccess = true;
            }

            return identityResponse;
        }
        public override async Task<SafeListCompanyResponse> GetSafeList(SafeListControlRequest request, ServerCallContext context)
        {
            var identityResponse = new SafeListCompanyResponse();


            var response = await _accountService.GetSafeList(request.EnterpriseId);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;
                response.Data.ForEach(x =>
                {
                    SafeListRequest.Types.ControlType controlType;
                    switch (x.ControlType)
                    {
                        case ControlType.IpAddress:
                            controlType = SafeListRequest.Types.ControlType.Ip;
                            break;
                        case ControlType.Mac:
                            controlType = SafeListRequest.Types.ControlType.Mac;
                            break;
                        case ControlType.DeviceId:
                            controlType = SafeListRequest.Types.ControlType.Other;
                            break;
                        default:
                            controlType = SafeListRequest.Types.ControlType.Ip;
                            break;
                    }
                    identityResponse.Data.Add(new SafeListRequest
                    {
                        Action = x.Action,
                        Address = x.Address,
                        Description = x.Description,
                        EnterpriseId = x.Enterpriseıd.ToString(),
                        TenantId = x.TenantId.ToString(),
                        Id = x.RecId.ToString(),
                        Name = x.Name ?? "",
                        ControlType = controlType
                    });
                });
            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }
            return identityResponse;
        }

        public override async Task<IdentityResponse> UpdateTenant(TenantUpdateRequest request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var response = await _accountService.UpdateTenantAsync(request.Id, request.UseSafeList);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }

            return identityResponse;
        }

        public override async Task<AccountDomainResponse> GetAccountDomains(AccountDomainModel request, ServerCallContext context)
        {
            var domainResponse = new AccountDomainResponse();
            var response = await _accountService.GetAccountDomains(request.TenantId);
            if (response.ResponseType == ResponseType.Ok)
            {
                response.Data.ForEach(x =>
                {
                    domainResponse.Data.Add(new AccountDomainModel
                    {
                        Id = x.RecId.ToString(),
                        Remark = x.Remark,
                        PropertyId = x.PropertyId.ToString(),
                        TenantId = x.TenantId.ToString()

                    });
                });

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                domainResponse.Message = context.Status.Detail;
            }

            return domainResponse;
        }

        public override async Task<IdentityResponse> CreateAccountDomain(AccountDomainModel request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();
            var domain = new AccountDomainDto
            {
                Remark = request.Remark,
                PropertyId = new Guid(request.PropertyId),
                TenantId = new Guid(request.TenantId),
                CreatedBy = "veboni",
                CreatedDate = DateTime.Now
            };
            var response = await _accountService.CreateAccountDomain(domain);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }

            return identityResponse;
        }

        public override async Task<IdentityResponse> UpdateAccountDomain(AccountDomainModel request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();
            var domain = new AccountDomainDto
            {
                RecId = new Guid(request.Id),
                Remark = request.Remark,
                PropertyId = new Guid(request.PropertyId),
                TenantId = new Guid(request.TenantId),
                CreatedBy = "veboni",
                CreatedDate = DateTime.Now
            };
            var response = await _accountService.UpdateAccountDomain(domain);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;

            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }
            return identityResponse;
        }

        public override async Task<IdentityResponse> DeleteAccountDomain(AccountDomainModel request, ServerCallContext context)
        {
            var identityResponse = new IdentityResponse();

            var response = await _accountService.DeleteAccounDomain(request.Id);
            if (response.ResponseType == ResponseType.Ok)
            {
                identityResponse.IsSuccess = true;
            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { response.Data.ToString() }));
                identityResponse.Message = context.Status.Detail;
            }
            return identityResponse;
        }

        public override async Task<AccountDetailModel> GetAccountDetailByAppKey(AccountDetailRequest request, ServerCallContext context)
        {
            var identityResponse = new AccountDetailModel();

            var user = await _accountService.GetSubscriptionApp(request.AppKey);
            if (user != null)
            {
                identityResponse.Connection = user.ConnectionDb;
                identityResponse.TenantId = user.TenantId.ToString();
                identityResponse.UserId = user.UserId;
            }
            else
            {
                context.Status = new Status(StatusCode.NotFound, string.Join(',', new string[] { "Kullanıcı bulunamadı" }));

            }
            return identityResponse;
        }
    }
}
