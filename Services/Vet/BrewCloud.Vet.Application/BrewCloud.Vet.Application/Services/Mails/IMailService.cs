using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Dtos.MailKit;
using BrewCloud.Shared.Events;
using BrewCloud.Vet.Application.Models.Mail;

namespace BrewCloud.Vet.Application.Services.Mails
{
    public interface IMailService
    {
        Task<Response<bool>> SendMail(string path, ISendMailRequestEvent request);
    }
}
