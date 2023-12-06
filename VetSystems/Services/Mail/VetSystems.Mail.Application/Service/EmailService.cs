using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Events;
using VetSystems.Shared.Service;

namespace VetSystems.Mail.Application.Service
{
    internal class EmailService : IEmailService
    {
        public Task<bool> SendEmail(EmailData emailData)
        {
            throw new NotImplementedException();
        }
    }
}
