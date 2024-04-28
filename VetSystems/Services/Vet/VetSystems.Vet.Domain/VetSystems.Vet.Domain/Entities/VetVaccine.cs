using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetVaccine : BaseEntity
    {
        public int AnimalType { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public int TimeDone { get; set; } //Nezamanyapilacak
        public Guid RenewalOption { get; set; } //YenilemeSecenegi
        public int Obligation { get; set; } //Zorundalık 
        public decimal? TotalSaleAmount { get; set; } = 0;
        public virtual ICollection<VetVaccineMedicine> VetVaccineMedicine { get; set; }
    }
}
