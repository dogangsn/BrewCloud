using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class AnimalColorsDef : BaseEntity, IAggregateRoot
    {
        public int rId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
