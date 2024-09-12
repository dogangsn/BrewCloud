using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Patients.Examinations
{
    public class ExaminationSalesListDto
    {
        public int Type { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Kdv { get; set; }
        public decimal Total { get; set; }
        public int PaymentType { get; set; }
    }
}
