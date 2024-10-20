﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetParameters : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public int? AppointmentReminderDuration { get; set; }
        public int? AgendaNoteReminder { get; set; }
        public string Days { get; set; } = string.Empty;
        public Guid? SmsCompany { get; set; }
        public Guid? CashAccount { get; set; }
        public Guid? CreditCardCashAccount { get; set; }
        public Guid? BankTransferCashAccount { get; set; }
        public Guid? WhatsappTemplate { get; set; }
        public Guid? CustomerWelcomeTemplate { get; set; }
        public Guid? AutomaticAppointmentReminderMessageTemplate { get; set; }
        public bool? IsOtoCustomerWelcomeMessage { get; set; }
        public bool? DisplayVetNo { get; set; } = false;
        public bool? AutoSms { get; set; } = false;
        public bool? IsAnimalsBreeds { get; set; } = false;
        public bool? IsFirstInspection { get; set; } = false;
        public string AppointmentBeginDate { get; set; } = string.Empty;
        public string AppointmentEndDate { get; set; } = string.Empty;
        public bool? IsExaminationAmuntZero { get; set; } = false;
        public int? DatetimeStatus { get; set; } = 0;
        public int? AppointmentInterval { get; set; } = 0;
        public int? AppointmentSeansDuration { get; set; } = 10;
        public int? PetHotelsDateTimeFormat { get; set; } = 0;

    }
}
