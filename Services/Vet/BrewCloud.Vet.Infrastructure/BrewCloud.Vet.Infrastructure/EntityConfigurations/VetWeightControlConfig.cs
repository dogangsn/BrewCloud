using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Infrastructure.EntityConfigurations
{
    public class VetWeightControlConfig : IEntityTypeConfiguration<VetWeightControl>
    {
        public void Configure(EntityTypeBuilder<VetWeightControl> entity)
        {
            entity.HasKey(e => e.Id)
                        .HasName("VetWeightControl_pkey");
        }
    }
}
