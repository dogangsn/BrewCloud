using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Customers
{
    public class PaymentTransactionListDto
    {
        public Guid Id { get; set; }
        public int AppointmentType { get; set; }
        public decimal? SellingPrice { get; set; }
        public Guid? Vaccineid { get; set; }
        public string? TextValue { get; set; }
        public bool? IsDefaultPrice { get; set; }
        public decimal? Price { get; set; }
        public Guid? TaxisId { get; set; }
    }
}
