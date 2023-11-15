using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetParameters : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public int? AppointmentReminderDuration { get; set; }
        public int? AgendaNoteReminder { get; set; }
        public string Days { get; set; }
        public Guid? SmsCompany { get; set; }
        public Guid? CashAccount { get; set; }
        public Guid? CreditCardCashAccount { get; set; }
        public Guid? BankTransferCashAccount { get; set; }
        public Guid? WhatsappTemplate { get; set; }
        public Guid? CustomerWelcomeTemplate { get; set; }
        public Guid? AutomaticAppointmentReminderMessageTemplate { get; set; }
        public bool? IsOtoCustomerWelcomeMessage { get; set; }
        public bool? DisplayVetNo { get; set; }
        public bool? AutoSms { get; set; }

    }
}
