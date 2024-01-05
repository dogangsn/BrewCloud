using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Contracts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Dtos.MailKit;
using VetSystems.Shared.Events;
using VetSystems.Vet.Application.Models.Mail;

namespace VetSystems.Vet.Application.Services.Mails
{
    public interface IMailService
    {
        Task<Response<bool>> SendMail(string path, ISendMailRequestEvent request);
    }
}
