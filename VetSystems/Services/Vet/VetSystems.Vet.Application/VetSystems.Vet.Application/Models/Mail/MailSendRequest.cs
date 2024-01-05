using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos.MailKit;

namespace VetSystems.Vet.Application.Models.Mail
{
    public class MailSendRequest
    {
        public string baseAddres { get; set; }
        public MailDetailDto mail { get; set; }
    }
}
