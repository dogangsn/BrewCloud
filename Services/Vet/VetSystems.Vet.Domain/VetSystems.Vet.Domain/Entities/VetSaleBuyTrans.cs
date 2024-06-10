using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetSaleBuyTrans : BaseEntity
    {
        public Guid OwnerId { get; set; }
        public Guid? ProductId { get; set; }
        public decimal? Ratio { get; set; } = 0;   
        public decimal? Amount { get; set; } = 0; 
        public decimal? Discount { get; set; } = 0; 
        public decimal? Price { get; set; } = 0;
        public decimal? NetPrice { get; set; } = 0;
        public string InvoiceNo { get; set; } = string.Empty;
        public bool? VatIncluded { get; set; } = true;
        public decimal? VatAmount { get; set; }
        public Guid? OrderId { get; set; }
        public int Quantity { get; set; }
        public Guid TaxisId { get; set; }

        [NotMapped]
        public bool IsNew { get; set; }

        public virtual VetSaleBuyOwner VetSaleBuyOwner { get; set; }
    }
}
