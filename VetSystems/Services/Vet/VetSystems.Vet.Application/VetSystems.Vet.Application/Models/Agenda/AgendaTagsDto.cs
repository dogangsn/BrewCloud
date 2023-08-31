using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Agenda
{
    public class AgendaTagsDto
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public int? Tags { get; set; }
    }
}
