using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Appointments
{
    public class AppointmentDailyListDto
    {
        public AppointmentDailyListDto()
        {
            VaccineItems = new List<VaccineListDto>();
        }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerPatientName { get; set; }
        public string Services { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? doctorId { get; set; } 
        public int AppointmentType { get; set; } 
        public bool? IsComplated { get; set; }
        public Guid? VaccineId { get; set; }
        public Guid? PatientsId { get; set; }
        public string Note { get; set; }
        public List<VaccineListDto>? VaccineItems { get; set; }
    }
}
