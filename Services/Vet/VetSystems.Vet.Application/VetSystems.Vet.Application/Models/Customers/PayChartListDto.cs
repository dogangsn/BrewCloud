using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class PayChartListDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Operation { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Paid { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal? Total { get; set; }
        public Guid? AppointmentId { get; set; }
    }
}
