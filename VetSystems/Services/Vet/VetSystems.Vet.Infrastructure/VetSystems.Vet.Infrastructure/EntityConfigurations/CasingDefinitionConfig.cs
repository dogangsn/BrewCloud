using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    internal class CasingDefinitionConfig : IEntityTypeConfiguration<CasingDefinition>
    {
        public void Configure(EntityTypeBuilder<CasingDefinition> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("CasingDefinition_pkey");
        }
    }
}
