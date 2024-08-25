using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.SaleBuy
{
    public class SaleBuyListDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string PaymentName { get; set; } = string.Empty;
        public decimal? NetPrice { get; set; }
        public decimal? Price { get; set; }
        public decimal? KDV { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? demandsGuidId { get; set; }
        public string Remark { get; set; } = string.Empty;
        public decimal? Amount { get; set; }
        public Guid ProductId { get; set; } 
        public string ProductName { get; set; } = string.Empty;
        public int PaymentType { get; set; }
        public bool IsPaymentCollected { get; set; }

    }
}
