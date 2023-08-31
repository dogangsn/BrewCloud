using System;
using System.Collections.Generic;
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
    }
}
