using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.Product
{
    public class ProductDescriptionsDto
    {
        public Guid Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductCode { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool Active { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SupplierId { get; set; }
        public decimal Ratio { get; set; }
        public decimal CriticalAmount { get; set; } = 0;
        public bool? SellingIncludeKDV { get; set; } = false;
        public bool? BuyingIncludeKDV { get; set; } = false;
        public bool? FixPrice { get; set; } = false;
        public bool? IsExpirationDate { get; set; } = false;
        public int? AnimalType { get; set; }
        public int? NumberRepetitions { get; set; }
        public Guid StoreId { get; set; } = Guid.Empty;
        public Guid? TaxisId { get; set; }
        public decimal? StockCount { get; set; } = 0;
        public string UnitName { get; set; }

    }
}
