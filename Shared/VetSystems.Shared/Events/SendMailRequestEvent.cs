using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.Shared.Contracts;

namespace VetSystems.Shared.Events
{
    public class SendMailRequestEvent : IntegrationBaseEvent , ISendMailRequestEvent
    {
        public Guid RecId { get; set; }
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string ConnectionString { get; set; }
    }
}
