using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Demands.DemandTrans
{
    public class DemandTransDto
    {
        public Guid id { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? StockState { get; set; }
        public int? isActive { get; set; }
        public decimal? Reserved { get; set; }
        public string Barcode { get; set; } = string.Empty;
    }
}
