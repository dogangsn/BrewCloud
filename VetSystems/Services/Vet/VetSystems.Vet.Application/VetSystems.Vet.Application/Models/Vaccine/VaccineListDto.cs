using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Vaccine
{
    public class VaccineListDto
    {
        public VaccineListDto()
        {
            VetVaccineMedicine = new List<VetVaccineMedicineListDto>();
        }
        public Guid Id { get; set; }
        public string AnimalType { get; set; }
        public string VaccineName { get; set; }
        public int TimeDone { get; set; } //Nezamanyapilacak
        public int RenewalOption { get; set; } //YenilemeSecenegi
        public int Obligation { get; set; } //Zorundalık 
        public decimal TotalSaleAmount { get; set; }
        public ICollection<VetVaccineMedicineListDto> VetVaccineMedicine { get; set; }
    }
}
