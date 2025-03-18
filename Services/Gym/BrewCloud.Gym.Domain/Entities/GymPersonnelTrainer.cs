using BrewCloud.Gym.Domain.Common;
using BrewCloud.Gym.Domain.Enums;

namespace BrewCloud.Gym.Domain.Entities
{
    public class GymPersonnelTrainer : BaseEntity
    {
        public Guid? BranchId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Experiences { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public short? Capacity { get; set; }
        public bool IsStaff { get; set; }
    }
}
