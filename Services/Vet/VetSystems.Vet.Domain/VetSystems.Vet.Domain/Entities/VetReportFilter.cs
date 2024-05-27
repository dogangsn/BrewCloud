using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetReportFilter: BaseEntity, IAggregateRoot
    {
        public Guid EnterprisesId { get; set; }
        public string Name { get; set; }
        public string FilterJson { get; set; }
    }
}
