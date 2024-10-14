using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Definition.Taxis
{
    public class TaxisDto
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public int TaxRatio { get; set; }
    }
}
