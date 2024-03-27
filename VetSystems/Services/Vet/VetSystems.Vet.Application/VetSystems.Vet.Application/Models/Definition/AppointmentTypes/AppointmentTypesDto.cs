using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.AppointmentTypes
{
    public class AppointmentTypesDto
    {
        public int Type { get; set; }
        public string Remark { get; set; } = string.Empty;
        public bool IsChange { get; set; } = false;
    }
}
