using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetAccomodation : BaseEntity
    {
        public int Type { get; set; }
        public Guid RoomId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public AccomodationType Accomodation { get; set; }
        public string Remark { get; set; } = string.Empty;
        public bool? IsLogOut { get; set; } = false;
    }


    public enum AccomodationType
    {
        Hostel = 1,
        Hospitalization = 2
    }
}
