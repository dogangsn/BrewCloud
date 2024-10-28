using BrewCloud.Gym.Domain.Common;

namespace BrewCloud.Gym.Domain.Entities
{
    public class GymMember : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? IdentityNumber { get; set; }
        public bool IsMember { get; set; }
        public bool IsMaried { get; set; }
        public string? District { get; set; }
        public string? Job { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }
        public byte? BloopType { get; set; }
        public string? EmergencyPerson { get; set; }
        public string? EmergencyPersonPhone { get; set; }
        public string? Note { get; set; }
        public string? CardNumber { get; set; }
        public Guid? PersonnelTrainerId { get; set; }
        public Guid BranchId { get; set; }
    }
}
