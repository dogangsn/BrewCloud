using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class CustomerDetailsDto
    {
        public Guid id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phonenumber { get; set; }
        public string? phonenumber2 { get; set; }
        public string email { get; set; }
        public string taxoffice { get; set; }
        public string vkntcno { get; set; }
        public Guid customergroup { get; set; }
        public string note { get; set; }
        public double discountrate { get; set; }
        public bool isemail { get; set; }
        public bool isphone { get; set; }
        public Guid adressid { get; set; }
        public DateTime createdate { get; set; }


    }
}
