using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class CustomersDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneNumber2 { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string VKNTCNo { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; } = 0;
        public bool? IsEmail { get; set; } = false;
        public bool? IsPhone { get; set; } = false;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string LongAdress { get; set; } = string.Empty;
        //public Guid FarmsId { get; set; }
        public  FarmsDto? FarmsDetail { get; set; }
        public List<PatientsDetailsDto> PatientDetails { get; set; }
        public int? PetCount { get; set; }
    }
}
