using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetSmsTemplate : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;
        public string Design { get; set; } = string.Empty;

    }
}
