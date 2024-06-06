using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Accounting
{
    public class SaleResponseDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
    }
}
