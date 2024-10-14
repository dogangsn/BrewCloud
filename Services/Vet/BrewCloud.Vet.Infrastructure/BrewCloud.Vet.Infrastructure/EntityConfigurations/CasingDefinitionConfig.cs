using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Infrastructure.EntityConfigurations
{
    internal class CasingDefinitionConfig : IEntityTypeConfiguration<VetCasingDefinition>
    {
        public void Configure(EntityTypeBuilder<VetCasingDefinition> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetCasingDefinition_pkey");
        }
    }
}
