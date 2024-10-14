using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetAnimalsType : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
    }
}
