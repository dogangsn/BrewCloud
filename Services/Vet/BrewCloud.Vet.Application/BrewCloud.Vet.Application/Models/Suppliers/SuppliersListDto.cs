using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Models.Definition.Suppliers
{
    public class SuppliersListDto
    {
        public Guid id{ get; set; }
        public string suppliername { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool active { get; set; }
        public string Adress { get; set; } = string.Empty;
        public InvoiceTpe InvoiceType { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string WebSite { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;

    }
}
