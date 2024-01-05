using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Dtos.MailKit
{
    public class MailDetailDto
    {
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
