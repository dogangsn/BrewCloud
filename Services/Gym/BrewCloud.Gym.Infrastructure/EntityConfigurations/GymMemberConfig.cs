using BrewCloud.Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrewCloud.Gym.Infrastructure.EntityConfigurations
{
    public class GymMemberConfig : IEntityTypeConfiguration<GymMember>
    {
        public void Configure(EntityTypeBuilder<GymMember> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("GymMember_pkey");

            //entity.Property(e => e.RecId)
            //    .ValueGeneratedOnAdd()
            //    .HasColumnName("recId")
            //    .UseIdentityColumn();
        }
    }
}
