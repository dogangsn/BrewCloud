using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.Product
{
    public class ProductMovementListDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal NetPrice { get; set; } 
    }
}
