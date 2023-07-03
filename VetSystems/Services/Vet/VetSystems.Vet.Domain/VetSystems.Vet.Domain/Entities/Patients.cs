using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class Patients : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? ChipNumber { get; set; }
        public int Sex { get; set; }
        public int AnimalType { get; set; }
        public int AnimalBreed { get; set; }
        public int AnimalColor { get; set; }
        public string? ReportNumber { get; set; }
        public string? SpecialNote { get; set; }
        public bool Sterilization { get; set; }
        public Customers Customers { get; set; }
    }
}
