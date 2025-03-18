using BrewCloud.Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewCloud.Gym.Infrastructure.EntityConfigurations
{
    public class GymPersonnelPermissionConfig : IEntityTypeConfiguration<GymPersonnelPermission>
    {
        public void Configure(EntityTypeBuilder<GymPersonnelPermission> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("GymPersonnelPermission_pkey");
        }
    }
}
