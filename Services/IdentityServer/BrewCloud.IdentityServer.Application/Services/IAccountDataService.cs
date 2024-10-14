using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.IdentityServer.Application.Models;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.IdentityServer.Application.Services
{
    public interface IAccountDataService
    {
        Task<Response<bool>> SeedData(ICreateSubscriptionRequestEvent model);
        Task<Response<bool>> SendAsync(string path, ICreateSubscriptionRequestEvent model);
        Task<Response<bool>> ImportExchangeRate(ExchangeRequestDto model);

        Task<Response<bool>> CreateDatabse(ICreateSubscriptionRequestEvent model);
    }
}
