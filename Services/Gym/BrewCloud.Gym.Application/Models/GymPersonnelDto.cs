namespace BrewCloud.Gym.Application.Models
{
    public class GymPersonnelDto
    {
        public string Id { get; set; }
        public Guid BranchId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? Email { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Graduate { get; set; }
        public string? Address { get; set; }
        public string? ImageId { get; set; }
    }
}
