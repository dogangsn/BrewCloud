using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetAdress : BaseEntity, IAggregateRoot
    {
        [NotMapped]
        public int RecId { get; set; }
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string LongAdress { get; set; } = string.Empty;

    }
}
