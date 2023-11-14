using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Patients
{
    public class PatientDetailsDto
    {
        public Guid RecId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ChipNumber { get; set; }
        public int Sex { get; set; }
        public string AnimalType { get; set; }
        public string BreedType { get; set; }
        public string AnimalColor { get; set; }
    }
}
