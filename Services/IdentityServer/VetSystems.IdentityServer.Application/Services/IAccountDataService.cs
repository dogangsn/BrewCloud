using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetSystems.IdentityServer.Application.Models;
using VetSystems.Shared.Contracts;
using VetSystems.Shared.Dtos;

namespace VetSystems.IdentityServer.Application.Services
{
    public interface IAccountDataService
    {
        Task<Response<bool>> SeedData(ICreateSubscriptionRequestEvent model);
        Task<Response<bool>> SendAsync(string path, ICreateSubscriptionRequestEvent model);
        Task<Response<bool>> ImportExchangeRate(ExchangeRequestDto model);

        Task<Response<bool>> CreateDatabse(ICreateSubscriptionRequestEvent model);
    }
}
