using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Contracts
{
    public interface ISendMailRequestEvent
    {
        public Guid RecId { get; set; }
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string ConnectionString { get; set; }
    }
}
