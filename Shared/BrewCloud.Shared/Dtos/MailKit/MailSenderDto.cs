using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.Dtos.MailKit
{
    public class MailSenderDto
    {
        public string DisplayName { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 0;
        public bool UseSSL { get; set; } = false;
    }
}
