using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetProducts : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SupplierId { get; set; }
        public string ProductBarcode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public decimal Ratio { get; set; } = 0;
        public decimal BuyingPrice { get; set; } = 0;
        public decimal SellingPrice { get; set; } = 0;
        public decimal CriticalAmount { get; set; } = 0;
        public bool? Active { get; set; } = true;
        public bool? SellingIncludeKDV { get; set; } = false;
        public bool? BuyingIncludeKDV { get; set; } = false;
        public bool? FixPrice { get; set; } = false;
        public bool? IsExpirationDate { get; set; } = false;
        public int? AnimalType { get; set; }
        public int? NumberRepetitions { get; set; }
        public Guid StoreId { get; set; } = Guid.Empty;
        public Guid? TaxisId { get; set; }


    }
}
