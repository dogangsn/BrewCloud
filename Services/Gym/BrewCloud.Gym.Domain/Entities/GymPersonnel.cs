using BrewCloud.Gym.Domain.Common;

namespace BrewCloud.Gym.Domain.Entities
{
    public class GymPersonnel : BaseEntity
    {
        public required string Name { get; set; }
        public required string SurName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? Email { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Graduate { get; set; }
        public string? Address { get; set; }
        public string? ImageId { get; set; }
        public required Guid BranchId { get; set; }
    }
}
