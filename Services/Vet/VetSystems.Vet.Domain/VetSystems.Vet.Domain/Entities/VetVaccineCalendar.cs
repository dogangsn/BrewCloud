using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetVaccineCalendar : BaseEntity
    {
        [NotMapped]
        public int RecId { get; set; }
        public int AnimalType { get; set; }
        public DateTime VaccineDate { get; set; }
        public bool IsDone { get; set; }
        public bool IsAdd { get; set; }
        public Guid PatientId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VaccineId { get; set; }
        public string VaccineName { get; set; } = string.Empty;
    }

}
