using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.StockTracking
{
    public class StockTrackingDto
    {
        public Guid Id { get; set; }
        public string ProcessTypeName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string ExpirationDateString { get; set; } = string.Empty;
        public decimal PurchasePrice { get; set; }
        public decimal RemainingPiece { get; set; }

    }
}
