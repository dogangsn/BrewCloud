using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Vaccine
{
    public class VetVaccineMedicineListDto
    {

        public Guid VaccineId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal SalesAmount { get; set; } = 0;
        public Guid TaxisId { get; set; }
        public int DosingType { get; set; } = 0;
        public string Remark { get; set; }
    }
}
