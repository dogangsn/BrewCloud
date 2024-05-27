using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Demands.Demands
{
    public class DemandsDto
    {
        public Guid id { get; set; }
        public DateTime? date { get; set; }
        public string documentno { get; set; } = string.Empty;
        public Guid? suppliers { get; set; }
        public DateTime? deliverydate { get; set; } 
        public string note { get; set; } = string.Empty;
        public int? state { get; set; }
        public bool?  isBuying{ get; set; }
        public bool?  isAccounting{ get; set; }
        public bool? iscomplated { get; set; } = false;
    }
}
