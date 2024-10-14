using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetAgendaTags : BaseEntity, IAggregateRoot
    {
        public Guid AgendaId { get; set; }
        public Guid? TagsId { get; set; } 
        public string Tags { get; set; } 
        //public VetAgenda Agenda { get; set; }
    }
}
