using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class Adress : BaseEntity, IAggregateRoot
    {
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string LongAdress { get; set; } = string.Empty;

    }
}
