using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Definition.CasingDefinition
{

    public class CasingDefinitionListDto
    {
        public Guid id { get; set; }
        public string casename { get; set; }
        public bool active { get; set; }
    }
}
