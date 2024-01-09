using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class PaymentTransactionListDto
    {
        public Guid Id { get; set; }
        public int AppointmentType { get; set; }
        public decimal? SellingPrice { get; set; }
        public string? TextValue { get; set; }
    }
}
