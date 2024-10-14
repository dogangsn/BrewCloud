using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetMessageLogs : BaseEntity
    {
        public DateTime SendDate { get; set; }
        public Guid Userİd { get; set; }
        public Guid CustomerId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int ItenrationType { get; set; } //SMS/WHP/Mail/Mobile
        public SendStatus Status { get; set; }
        public Guid IntegrationId { get; set; }
    }

 

    public enum SendStatus
    {
        Send = 1,
        UnSend = 2,
        Waiting = 3,
        Error = 4,
    }
}


