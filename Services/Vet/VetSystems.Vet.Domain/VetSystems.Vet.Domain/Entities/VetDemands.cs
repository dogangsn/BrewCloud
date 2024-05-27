using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetDemands : BaseEntity, IAggregateRoot
    {
        public DateTime? date  { get; set; }
        public string documentno { get; set; }
        public Guid? suppliers { get; set;}
        public DateTime? deliverydate { get; set; }
        public string note { get; set;}
        public int? state { get; set; }
        public bool? isBuying { get; set; }
        public bool? isAccounting { get; set; }
        public bool? iscomplated { get; set; }

    }
}
