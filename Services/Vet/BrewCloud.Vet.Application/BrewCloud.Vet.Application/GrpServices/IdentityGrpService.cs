using Identity.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.GrpServices
{
    public class IdentityGrpService
    {

        private readonly IdentityUserProtoService.IdentityUserProtoServiceClient _identityProtoService;

        public IdentityGrpService(IdentityUserProtoService.IdentityUserProtoServiceClient identityProtoService)
        {
            _identityProtoService = identityProtoService ?? throw new ArgumentNullException(nameof(identityProtoService));
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
        public async Task<IdentityResponse> CheckSafeListAsync(SafeListControlRequest request)
        {
            return await _identityProtoService.CheckSafeListAsync(request);
        }

    }
}
