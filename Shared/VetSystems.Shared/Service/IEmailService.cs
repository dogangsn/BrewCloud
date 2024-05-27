using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Events;

namespace VetSystems.Shared.Service
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailData emailData);
    }
}
