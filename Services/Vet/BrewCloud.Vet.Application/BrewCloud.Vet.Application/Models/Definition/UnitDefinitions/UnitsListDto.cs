using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Definition.UnitDefinitions
{
    public class UnitsListDto
    {
        public Guid Id { get; set; }
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
    }
}
