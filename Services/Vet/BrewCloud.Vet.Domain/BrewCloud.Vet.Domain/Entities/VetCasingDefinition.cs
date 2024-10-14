using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetCasingDefinition : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public string CaseName { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public string Kasa { get; set; } = string.Empty;
        public bool? Durumu { get; set; }
    }
}
