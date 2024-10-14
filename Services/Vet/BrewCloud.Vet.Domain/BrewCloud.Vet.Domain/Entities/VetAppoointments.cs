using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;
namespace BrewCloud.Vet.Domain.Entities
{
    public class VetAppointments : BaseEntity, IAggregateRoot
    {
         
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; } = string.Empty;
        public Guid? DoctorId { get; set; } = Guid.Empty;
        public Guid? CustomerId { get; set; } 
        public Guid? PatientsId { get; set; } = Guid.Empty;
        public int? AppointmentType { get; set; } 
        public bool? IsCompleted { get; set; } = false;
        public Guid? VaccineId { get; set; } = Guid.Empty;
        public bool? IsPaymentReceived { get; set; } = false;
        public StatusType Status { get; set; }
        public bool? IsMessage { get; set; } = false;
    }

    public enum StatusType
    {
        Waiting = 1, //Bekliyor
        Canceled = 2, //IptalEdildi
        Discussed = 3, //Gorusuldu
        NotCome = 4, //Gelmedi
    }



}
