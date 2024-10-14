using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Agenda
{
    public class AgendaTagsDto
    {
        public Guid Id { get; set; }
        public Guid AgendaId { get; set; }
        public Guid? TagsId { get; set; }
        public string Tags { get; set; }
    }
}
