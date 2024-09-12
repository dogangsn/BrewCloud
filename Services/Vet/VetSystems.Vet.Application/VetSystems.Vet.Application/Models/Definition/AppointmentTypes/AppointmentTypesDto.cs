using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.AppointmentTypes
{
    public class AppointmentTypesDto
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string Remark { get; set; } = string.Empty;
        public bool IsChange { get; set; } = false;
        public bool IsDefaultPrice { get; set; } = false;
        public decimal Price { get; set; }
        public Guid? TaxisId { get; set; }
        public string Colors { get; set; } = string.Empty;
    }
}
