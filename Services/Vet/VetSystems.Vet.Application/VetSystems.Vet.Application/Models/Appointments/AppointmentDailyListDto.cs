using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Appointments
{
    public class AppointmentDailyListDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerPatientName { get; set; }
        public string Services { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
    }
}
