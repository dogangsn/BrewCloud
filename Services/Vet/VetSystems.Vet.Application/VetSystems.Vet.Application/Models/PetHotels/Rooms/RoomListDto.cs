using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Models.PetHotels.Rooms
{
    public class RoomListDto
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public RoomRriceType PricingType { get; set; }
    }
}
