using BrewCloud.Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewCloud.Gym.Infrastructure.EntityConfigurations
{
    public class GymPersonnelConfig : IEntityTypeConfiguration<GymPersonnel>
    {
        public void Configure(EntityTypeBuilder<GymPersonnel> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("GymPersonnel_pkey");
        }
    }
}
