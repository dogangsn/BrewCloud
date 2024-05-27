using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class PatientsDetailsDto
    {
        public string Name { get; set; } = string.Empty;
        public string BirthDate { get; set; }
        public string ChipNumber { get; set; } = string.Empty;
        public int Sex { get; set; }
        public int? AnimalType { get; set; }
        public int? AnimalBreed { get; set; }
        public int? AnimalColor { get; set; } 
        public string MyProperty { get; set; } = string.Empty;
        public string ReportNumber { get; set; } = string.Empty;
        public string SpecialNote { get; set; } = string.Empty;
        public bool Sterilization { get; set; }
        public bool Active { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
    }
}
