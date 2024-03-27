using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetAppointmentTypes : BaseEntity
    {
        public int Type { get; set; }
        public string Remark { get; set; } = string.Empty;
        public bool IsChange { get; set; } = false;

    }
}
