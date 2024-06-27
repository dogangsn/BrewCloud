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
        public Guid? SourceId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}
