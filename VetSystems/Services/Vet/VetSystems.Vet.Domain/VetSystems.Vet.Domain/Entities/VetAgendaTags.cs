using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetAgendaTags : BaseEntity, IAggregateRoot
    {
        public Guid AgendaId { get; set; }
        public int? Tags { get; set; } 
        public VetAgenda Agenda { get; set; }
    }
}
