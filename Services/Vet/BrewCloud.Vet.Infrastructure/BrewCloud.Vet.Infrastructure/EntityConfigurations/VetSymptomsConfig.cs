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
    internal class VetSymptomsConfig : IEntityTypeConfiguration<VetSymptoms>
    {
        public void Configure(EntityTypeBuilder<VetSymptoms> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetSymptoms_pkey");
        }
    }
}
