using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.Vet.Application.Services.Message.Sms
{
    public interface ISmsService
    {
        Task<Response<bool>> SendSms(ISendMessageRequest request);
    }
}
