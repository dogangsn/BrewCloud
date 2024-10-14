using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetProductMovements : BaseEntity
    {
        public int Type { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal? TotalAmount { get; set; }
        public int Quantity { get; set; }
        public Guid DepotId { get; set; }
    }
}
