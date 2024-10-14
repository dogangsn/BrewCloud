using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Agenda
{
    public class AgendaDto
    {
        public Guid id { get; set; }
        public int? AgendaNo { get; set; }
        public int? AgendaType { get; set; }
        public int? IsActive { get; set; }
        public string AgendaTitle { get; set; } = string.Empty;
        public int? Priority { get; set; } 
        public DateTime? DueDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public virtual List<AgendaTagsDto> AgendaTags { get; set; }

    }

}
