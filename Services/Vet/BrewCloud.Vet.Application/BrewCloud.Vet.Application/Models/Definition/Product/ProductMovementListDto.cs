using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Definition.Product
{
    public class ProductMovementListDto
    {
        public Guid ProductId { get; set; } 
        public int Type { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal PieceQuentity { get; set; }
        public string CustomerSupplier { get; set; }
    }
}
