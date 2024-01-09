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
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public string PaymentName { get; set; }
        public decimal? NetPrice { get; set; }
        public decimal? KDV { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? CustomerId { get; set; }

        public Guid? demandsGuidId { get; set; }


    }
}
