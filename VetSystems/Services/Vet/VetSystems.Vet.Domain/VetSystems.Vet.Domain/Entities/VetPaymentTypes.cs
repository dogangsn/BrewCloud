using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetPaymentTypes : IdBaseEntity
    {
        public string Value { get; set; } = string.Empty;
    }
}
