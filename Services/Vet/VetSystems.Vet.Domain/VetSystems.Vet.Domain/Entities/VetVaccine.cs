using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetVaccine : BaseEntity
    {
        public VetVaccine()
        {
            VetVaccineMedicine = new HashSet<VetVaccineMedicine>();
        }

        [NotMapped]
        public int RecId { get; set; }
        public int AnimalType { get; set; }
        public string VaccineName { get; set; } = string.Empty;
        public int TimeDone { get; set; } //Nezamanyapilacak
        public RenewalOption RenewalOption { get; set; } //YenilemeSecenegi
        public int Obligation { get; set; } //Zorundalık 
        public decimal? TotalSaleAmount { get; set; } = 0;
        public virtual ICollection<VetVaccineMedicine> VetVaccineMedicine { get; set; }
    }

    public enum RenewalOption
    {
        Tek = 1,
        Haftalik1 = 2,
        Aylik1 = 3,
        Yillik1 = 4,
        Haftalik2 = 5,
        Haftalik3 = 6,
        Haftalik10 = 7,
        Aylik2 = 8,
        Aylik3 = 9,
        Aylik4 = 10,
        Aylik6 = 11,
        Aylik8 = 12,
        Yillik2 = 13,
        Yillik3 = 14,
        Yillik4 = 15,
        Gunluk45 = 16
    }
}
