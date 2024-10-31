using BrewCloud.Gym.Domain.Common;
using BrewCloud.Gym.Domain.Enums;

namespace BrewCloud.Gym.Domain.Entities
{
    public class GymPersonnelPermission : BaseEntity
    {
        public bool IsApproved { get; set; }
        public Guid PersonnelId { get; set; }
        public PermissionType PermissionType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Note { get; set; }
    }
}
