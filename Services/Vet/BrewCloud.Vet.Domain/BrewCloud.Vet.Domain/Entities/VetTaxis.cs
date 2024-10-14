using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetTaxis : BaseEntity
    {
        public int Type { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public int TaxRatio { get; set; }
    }
}
