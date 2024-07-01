using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetDocuments : BaseEntity
    {
        public Guid? SourceId { get; set; } = Guid.Empty;
        public string FileName { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = new byte[0];
        public string Size { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
