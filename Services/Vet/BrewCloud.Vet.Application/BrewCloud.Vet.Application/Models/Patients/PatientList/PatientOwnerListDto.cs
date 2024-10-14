using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Patients.PatientList
{
    public class PatientOwnerListDto
    {
        public string CustomerFirsLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AnimalTypeName { get; set; }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string ChipNumber { get; set; }
        public int Sex { get; set; }
        public string AnimalType { get; set; }
        public string BreedType { get; set; }
        public string AnimalColor { get; set; }
        public string ReportNumber { get; set; }
        public string SpecialNote { get; set; }
        public bool Sterilization { get; set; }
        public bool Active { get; set; }
        public string Thumbnail { get; set; } = string.Empty;
        public byte[] Images { get; set; }
    }
}
