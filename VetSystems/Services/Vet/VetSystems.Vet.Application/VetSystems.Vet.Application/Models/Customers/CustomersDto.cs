using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class CustomersDto
    {
        public string FisrName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public int PhoneNumber2 { get; set; }
        public string EMail { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public int VKNTCNo { get; set; }
        public Guid CustomerGroup { get; set; }
        public string Note { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; } = 0;
        public bool? IsEmail { get; set; } = true;
        public bool? IsPhone { get; set; } = true;
    }
}
