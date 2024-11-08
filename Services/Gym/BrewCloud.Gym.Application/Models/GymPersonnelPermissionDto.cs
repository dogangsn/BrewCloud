using BrewCloud.Gym.Domain.Enums;

namespace BrewCloud.Gym.Application.Models
{
    public class GymPersonnelPermissionDto
    {
        public Guid Id { get; set; }
        public Guid PersonnelId { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public PermissionType PermissionType { get; set; }
        public string PermissionTypeName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Note { get; set; }
    }
}
