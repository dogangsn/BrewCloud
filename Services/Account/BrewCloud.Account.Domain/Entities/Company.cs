using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string CompanyTitle { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public int? TaxNumber { get; set; }
        public string TaxOffice { get; set; } = string.Empty;
        public int? DefaultInvoiceType { get; set; }
        public byte? CompanyImage { get; set; }
        public string BuildingName { get; set; } = string.Empty;
        public int? BuildingNumber { get; set; }
        public string City { get; set; } = string.Empty;
        public bool? InvoiceAmountNotes { get; set; }
        public bool? InvoiceNoAutoCreate { get; set; }
        public bool? InvoiceSendEMail { get; set; }
    }
}
