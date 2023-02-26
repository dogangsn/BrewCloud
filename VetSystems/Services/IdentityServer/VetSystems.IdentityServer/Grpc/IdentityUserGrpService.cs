using Grpc.Core;
using Identity.Api;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VetSystems.Identity.Infrastructure.Entities;
using VetSystems.Identity.Infrastructure.Services.Interface;

namespace VetSystems.IdentityServer.Grpc
{
    public class IdentityUserGrpService: IdentityUserProtoService.IdentityUserProtoServiceBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;

        public IdentityUserGrpService(UserManager<ApplicationUser> userManager, IAccountService accountService, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _accountService = accountService;
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
        }

        public override async Task<UserResponse> GetUserById(UserRequest request, ServerCallContext context)
        {
            var userResponse = new UserResponse();

            var user = await _userManager.FindByIdAsync(request.UserId);

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
            userResponse.Roleid = user.Account.RoleId;
            userResponse.AccountType = (int)user.Account.AccountType;
            userResponse.IsLicenceAccount = user.Account.IsLicenceAccount;
            return userResponse;
        }

    }
}
