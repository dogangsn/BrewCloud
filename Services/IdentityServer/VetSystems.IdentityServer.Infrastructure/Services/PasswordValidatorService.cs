using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Services.Interface;

namespace VetSystems.IdentityServer.Infrastructure.Services
{
    public class PasswordValidatorService : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountService _accountService;
        private IHttpContextAccessor _httpContextAccessor;

        public PasswordValidatorService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if (existUser == null)
            {
                var error = new Dictionary<string, object>();
                error.Add("errors", new List<string> { "Email veya şifre yanlış" });
                context.Result.CustomResponse = error;
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (passwordCheck == false)
            {
                var error = new Dictionary<string, object>();
                error.Add("errors", new List<string> { "Email veya şifre yanlış" });
                context.Result.CustomResponse = error;
                return;
            }

            string ip = string.Empty;
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("CF-Connecting-IP"))
            {
                ip = _httpContextAccessor.HttpContext.Request.Headers["CF-Connecting-IP"];
            }
            else
            {
                ip = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
            }
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("VB-DeviceId"))
            {
                ip = _httpContextAccessor.HttpContext.Request.Headers["VB-DeviceId"];
            }

            var service = GetService();
            var accountresponse = await service.CheckUserAccount(existUser, ip, context.Request.ClientId);
            if (accountresponse != null && accountresponse.ResponseType == Shared.Dtos.ResponseType.Error)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { accountresponse.Data });
                context.Result.CustomResponse = errors;
                return;
            }
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }

        public IProfileService GetService()
        {
            IProfileService service;
            service = new AdminProfileService(_accountService);
            return service;
        }
    }
}
