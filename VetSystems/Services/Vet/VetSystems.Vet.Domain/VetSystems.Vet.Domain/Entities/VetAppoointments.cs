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
        public string DoctorId { get; set; } = string.Empty;
        public string CustomerId { get; set; }

    }
}
