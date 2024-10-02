using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Lab
{
    public class LabDocumentDetailDto
    {
        public LabDocumentDetailDto()
        {
            LabDocuments = new List<LabDocumentDto>();
        }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PatientId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string PatientType { get; set; } = string.Empty;
        public List<LabDocumentDto> LabDocuments { get; set; }
    }

    public class LabAnimalDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class LabCustomerDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }


}
