using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Contracts;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Services.Message.Sms
{
    public interface ISmsService
    {
        Task<Response<bool>> SendSms(ISendMessageRequest request);
    }
}
