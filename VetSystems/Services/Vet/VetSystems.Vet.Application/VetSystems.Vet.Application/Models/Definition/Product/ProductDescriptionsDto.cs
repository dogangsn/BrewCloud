using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.Product
{
    public class ProductDescriptionsDto
    {
        public Guid Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductCode { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool Active { get; set; }

    }
}
