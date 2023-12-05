using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Account.Application.Models.Settings
{
    public class TitleDefinationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool? IsAppointmentShow { get; set; } = false;
    }
}
