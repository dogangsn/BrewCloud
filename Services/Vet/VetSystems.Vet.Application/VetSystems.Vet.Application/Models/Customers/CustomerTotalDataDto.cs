using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class CustomerTotalDataDto
    {
        public int TotalSaleBuyCount { get; set; }
        public int TotalVisitCount { get; set; }
        public decimal? TotalEarnings { get; set; }
        public decimal? TotalCollection { get; set; }
    }
}
