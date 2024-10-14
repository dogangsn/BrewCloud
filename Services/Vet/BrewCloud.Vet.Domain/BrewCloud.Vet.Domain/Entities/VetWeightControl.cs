using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetWeightControl : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public double Weight { get; set; }
        public DateTime ControlDate { get; set; }
    }
}
