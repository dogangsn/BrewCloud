using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public required string Name { get; set; }
        public string? District { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
