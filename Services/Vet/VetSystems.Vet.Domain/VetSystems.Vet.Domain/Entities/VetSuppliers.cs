using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetSuppliers : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public InvoiceTpe InvoiceType { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string MersisNo { get; set; } = string.Empty;
        public string WebSite { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
    
    public enum InvoiceTpe
    {
        Institutional = 1, //Kurumsal
        Individual = 2 //Bireyysel
    }
}
