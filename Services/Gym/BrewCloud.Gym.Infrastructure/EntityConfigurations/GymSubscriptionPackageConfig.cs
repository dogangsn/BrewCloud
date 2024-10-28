using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Gym.Domain.Entities;

namespace BrewCloud.Gym.Infrastructure.EntityConfigurations
{
    public class GymSubscriptionPackageConfig : IEntityTypeConfiguration<GymSubscriptionPackage>
    {
        public void Configure(EntityTypeBuilder<GymSubscriptionPackage> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("GymSubscriptionPackage_pkey");
        }
    }
}
