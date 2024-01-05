using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Clinicalstatistics
{
    public class ClinicalstatisticsListDto
    {
        public string Total { get; set; }
        public int? paymentType { get; set; }
        public int? Year { get; set; }
        public string Month { get; set; }
        public Guid? customerId { get; set; }
        public int? RequestType { get; set; }
        public int? Type { get; set; }
    } 
    public class ClinicalstatisticsListResponseDto
    {
        public ClinicalstatisticsListDto ThisWeekCustomerTotal { get; set; }
        public ClinicalstatisticsListDto PaymentTypeTotal { get; set; }
        public ClinicalstatisticsListDto PaymentTypeYearsTotal { get; set; }
    }
}
