using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Vaccine
{
    public class ShortCutListDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string link { get; set; }
        public bool useRouter { get; set; }
    }
}
