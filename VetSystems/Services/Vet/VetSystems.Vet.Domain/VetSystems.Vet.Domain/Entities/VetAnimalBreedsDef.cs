using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetAnimalBreedsDef : IdBaseEntity, IAggregateRoot
    {
        public int AnimalType { get; set; }
        public string BreedName { get; set; } = string.Empty;
    }
}
