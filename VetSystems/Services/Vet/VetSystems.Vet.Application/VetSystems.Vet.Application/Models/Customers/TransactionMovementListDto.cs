using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class TransactionMovementListDto
    {
        public int OperationNumber { get; set; }
        public DateTime Date { get; set; }
        public string? PaymentType { get; set; }
        public string? Note { get; set; }
        public decimal? Amount { get; set; }
    }
}
