using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Accounts;

namespace VetSystems.Identity.Infrastructure.Services.Interface
{
    public interface IAccountService
    {
        Task<SignupDto> GetAccountByIdForClaims(string id);
    }
}
