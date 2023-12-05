using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;
namespace VetSystems.Vet.Domain.Entities
{
    public class VetAppointments : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public Guid RecId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; } = string.Empty;
        public Guid? DoctorId { get; set; } 
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public int? AppointmentType { get; set; }
        public bool? IsCompleted { get; set; }

    }
}
