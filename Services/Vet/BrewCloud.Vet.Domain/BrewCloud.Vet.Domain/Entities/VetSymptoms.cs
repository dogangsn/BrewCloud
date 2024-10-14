using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetSymptoms : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public int RecId { get; set; }
        public string Symptom { get; set; }

    }
}
