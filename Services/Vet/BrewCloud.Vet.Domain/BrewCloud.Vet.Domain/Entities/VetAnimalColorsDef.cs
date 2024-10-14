using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetAnimalColorsDef : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
