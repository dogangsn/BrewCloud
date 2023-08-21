using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class CasingDefinition : BaseEntity, IAggregateRoot
    {
<<<<<<< Updated upstream
        //public Guid id { get; set; } = Guid.Empty;
        public string CaseName { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
=======
        public string Kasa { get; set; } = string.Empty;

        public bool? Durumu { get; set; }

>>>>>>> Stashed changes
    }
}
