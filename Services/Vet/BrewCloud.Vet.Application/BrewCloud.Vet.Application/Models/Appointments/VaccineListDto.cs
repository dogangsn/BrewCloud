using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Appointments
{
    public class VaccineListDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid? ProductId { get; set; }         
        public bool IsComplated { get; set; }
    }
}
