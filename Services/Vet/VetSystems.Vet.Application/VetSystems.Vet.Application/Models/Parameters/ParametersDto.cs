using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Parameters
{
    public class ParametersDto
    {
        public Guid? id{ get; set; }
        public int? appointmentReminderDuration { get; set; }
        public int? agendaNoteReminder { get; set; }
        public string days { get; set; } = string.Empty;
        public Guid? smsCompany { get; set; }
        public Guid? cashAccount { get; set; }
        public Guid? creditCardCashAccount { get; set; }
        public Guid? bankTransferCashAccount { get; set; }
        public Guid? whatsappTemplate { get; set; }
        public Guid? customerWelcomeTemplate { get; set; }
        public Guid? automaticAppointmentReminderMessageTemplate { get; set; }
        public bool? isOtoCustomerWelcomeMessage { get; set; } = false;
        public bool? displayVetNo { get; set; } = false; 
        public bool? autoSms { get; set; } = false;
        public bool? IsAnimalsBreeds { get; set; }
        public bool? IsFirstInspection { get; set; } = false;
        public string appointmentBeginDate { get; set; }
        public string appointmentEndDate { get; set; }
        public bool? IsExaminationAmuntZero { get; set; }
        public int? datetimestatus { get; set; }
        public int? appointmentinterval { get; set; }
        public int? appointmentSeansDuration { get; set; }
    }
}
