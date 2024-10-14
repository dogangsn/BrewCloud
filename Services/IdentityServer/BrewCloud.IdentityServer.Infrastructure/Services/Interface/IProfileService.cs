using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.IdentityServer.Infrastructure.Entities;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.IdentityServer.Infrastructure.Services.Interface
{
    public interface IProfileService
    {
        Task GetProfileDataAsync(ProfileDataRequestContext context);

        Task<Response<string>> CheckUserAccount(ApplicationUser user, string address, string clientId);
    }
}
