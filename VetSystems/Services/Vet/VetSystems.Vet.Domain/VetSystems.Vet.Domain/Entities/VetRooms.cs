using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetRooms : BaseEntity
    {
        public string RoomName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public RoomRriceType PricingType { get; set; }
    }

    public enum RoomRriceType
    {
        Daily = 1,
        Hour = 2,
    }


}
