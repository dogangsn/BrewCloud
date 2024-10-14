using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{ 
    public class VetPaymentCollection : BaseEntity, IAggregateRoot
    {
        public Guid? CustomerId { get; set; } = Guid.Empty;
        public Guid? CollectionId { get; set; }
        public DateTime? Date { get; set; }
        public string? Remark { get; set; } = string.Empty;
        public decimal? Debit { get; set; } = 0;
        public decimal? Credit { get; set; } = 0;
        public decimal? Paid { get; set; } = 0;
        public decimal? TotalPaid { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public Guid? SaleBuyId { get; set; } = Guid.Empty;
        public int? PaymetntId { get; set; }

    }
}
