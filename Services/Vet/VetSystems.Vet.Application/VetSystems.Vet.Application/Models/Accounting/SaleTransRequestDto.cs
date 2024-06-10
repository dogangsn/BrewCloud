using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Accounting
{
    public class SaleTransRequestDto
    {
        public Guid Id { get; set; }
        public Guid Product { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string Vat { get; set; }
    }
}
