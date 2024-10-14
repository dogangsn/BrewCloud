using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Lab
{
    public class LabDocumentDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PatientId { get; set; }
        public bool? IsRead { get; set; } = false;
        public string FileName { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = new byte[0];
        public string Remark { get; set; } = string.Empty;
    }
}
