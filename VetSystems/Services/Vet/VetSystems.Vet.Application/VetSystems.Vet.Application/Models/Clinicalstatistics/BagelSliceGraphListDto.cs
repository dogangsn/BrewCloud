using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Clinicalstatistics
{
    public class BagelSliceGraphListDto
    {
        public string id { get; set; }
        public Guid? GuidId { get; set; }
        public int? Counts { get; set; }
        public int? Types { get; set; }

    }
}
