using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetParameters : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public bool? AutoSms { get; set; }
    }
}
