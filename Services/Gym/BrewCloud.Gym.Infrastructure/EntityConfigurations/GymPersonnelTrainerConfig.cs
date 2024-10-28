using BrewCloud.Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewCloud.Gym.Infrastructure.EntityConfigurations
{
    public class GymPersonnelTrainerConfig : IEntityTypeConfiguration<GymPersonnelTrainer>
    {
        public void Configure(EntityTypeBuilder<GymPersonnelTrainer> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("GymPersonnelTrainer_pkey");
        }
    }
}
