using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.PetHotels.Accomodation
{
    public class AccomodationListDto
    {
        public string CustomerName { get; set; }
        public string RoomName { get; set; }
        public Guid RoomId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public DateTime checkinDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int Accomodation { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Createusers { get; set; }
        public int Type { get; set; }
        public Guid Id { get; set; }
    }
}
