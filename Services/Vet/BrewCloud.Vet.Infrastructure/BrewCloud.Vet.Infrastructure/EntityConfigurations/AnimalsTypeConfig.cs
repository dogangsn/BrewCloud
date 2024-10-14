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
    public class AnimalsTypeConfig : IEntityTypeConfiguration<VetAnimalsType>
    {
        public void Configure(EntityTypeBuilder<VetAnimalsType> entity)
        {
            entity.HasKey(e => e.RecId)
                    .HasName("VetAnimalsType_pkey");

            entity.Property(e => e.RecId)
                    .HasColumnName("RecId")
                    .UseIdentityColumn();

        }
    }
}
