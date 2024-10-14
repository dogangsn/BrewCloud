using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetShortcut : BaseEntity, IAggregateRoot
    {

        [NotMapped]
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string link { get; set; }
        public bool useRouter { get; set; }
    }
}
