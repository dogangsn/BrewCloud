using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Domain.Common;

namespace BrewCloud.Mail.Domain.Entities
{
    public class SmtpSetting : BaseEntity
    {
        public bool Defaults { get; set; } = false;
        public string DisplayName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 0;
        public bool UseSSL { get; set; } = false;
    }
}
