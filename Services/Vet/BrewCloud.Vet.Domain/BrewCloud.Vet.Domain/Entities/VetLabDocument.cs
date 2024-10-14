using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetLabDocument : BaseEntity, IAggregateRoot
    {
        public Guid CustomerId { get; set; }
        public Guid PatientId { get; set; }
        public bool? IsRead { get; set; } = false;
        public string FileName { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = new byte[0];
        public string Remark { get; set; } = string.Empty;
    }
}
