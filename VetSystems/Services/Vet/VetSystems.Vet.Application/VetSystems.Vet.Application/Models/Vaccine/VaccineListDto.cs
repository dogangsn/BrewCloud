using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Vaccine
{
    public class VaccineListDto
    {
        public string AnimalTypeName { get; set; }
        public string VaccineName { get; set; }
        public int TimeDone { get; set; } //Nezamanyapilacak
        public Guid RenewalOption { get; set; } //YenilemeSecenegi
        public int Obligation { get; set; } //Zorundalık
        public int VaccineType { get; set; } //AsiTuru
    }
}
