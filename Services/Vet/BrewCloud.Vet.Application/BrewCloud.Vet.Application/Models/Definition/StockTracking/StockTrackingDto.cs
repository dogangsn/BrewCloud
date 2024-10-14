using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Definition.StockTracking
{
    public class StockTrackingDto
    {
        public Guid Id { get; set; }
        public string ProcessTypeName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string ExpirationDateString { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal RemainingPiece { get; set; }
        public decimal Piece { get; set; }
        public int ProcessType { get; set; }
        public Guid SupplierId { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
