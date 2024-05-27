using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{

    public class VetAgenda : BaseEntity, IAggregateRoot
    {
        //public VetAgenda()
        //{
        //     AgendaTags = new HashSet<VetAgendaTags>();
        //}
        public int? AgendaNo { get; set; } 
        public int? AgendaType { get; set; } 
        public int? IsActive { get; set; } 
        public string AgendaTitle { get; set; } = string.Empty;
        public int? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        //public virtual ICollection<VetAgendaTags> AgendaTags { get; set; }
        //public virtual List<VetAgendaTags> AgendaTags { get; set; }

    }
}
