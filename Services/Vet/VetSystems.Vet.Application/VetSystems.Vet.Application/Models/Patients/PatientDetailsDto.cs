using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Patients
{
    public class PatientDetailsDto
    {
        public Guid id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string ChipNumber { get; set; }
        public int Sex { get; set; }
        public int AnimalType { get; set; }
        public string AnimalTypeName { get; set; } = string.Empty;
        public int? AnimalBreed { get; set; }
        public string BreedType { get; set; } = string.Empty;
        public string AnimalColor { get; set; } = string.Empty;
        public string ReportNumber { get; set; } = string.Empty;
        public string SpecialNote { get; set; } = string.Empty;
        public bool Sterilization { get; set; } 
        public bool Active { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public byte[] Images { get; set; }
    }
}
