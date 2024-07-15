using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetSmsTemplate : BaseEntity
    {
        public bool Active { get; set; } = false;
        public SmsType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool? EnableSMS { get; set; } = false;
        public bool? EnableAppNotification { get; set; } = false;
        public bool? EnableEmail { get; set; } = false;
        public bool? EnableWhatsapp { get; set; } = false;

    }


    public enum SmsType
    {
        AppointmentReminder = 1,
        PaymentReminder = 2,   
    }
}
