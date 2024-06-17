using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class SalesCustomerListDto
    {
        public Guid SaleOwnerId { get; set; }
        public Guid CollectionId { get; set; }
        public DateTime Date { get; set; }
        public string SalesContent { get; set; }
        public decimal Amount { get; set; }
        public decimal Collection { get; set; }
        public decimal RameiningBalance { get; set; }
        public Guid ExaminationsId { get; set; }
        public bool IsExaminations { get; set; }

    }
}
