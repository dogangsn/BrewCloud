using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetVaccineMedicine : BaseEntity
    {
        public Guid VaccineId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal SalesAmount { get; set; } = 0;
        public Guid TaxisId { get; set; }
        public int DosingType { get; set; } = 0;
        public string Remark { get; set; }
        public VetVaccine VetVaccine { get; set; }
    }
}
