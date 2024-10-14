using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Events;

namespace BrewCloud.Shared.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailData emailData);
    }
}
