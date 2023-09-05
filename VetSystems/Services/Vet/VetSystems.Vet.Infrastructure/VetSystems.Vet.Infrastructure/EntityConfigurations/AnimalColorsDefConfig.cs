using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;
using static Dapper.SqlMapper;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    public class AnimalColorsDefConfig : IEntityTypeConfiguration<VetAnimalColorsDef>
    {
        public void Configure(EntityTypeBuilder<VetAnimalColorsDef> entity)
        {
            entity.HasKey(e => e.RecId)
                .HasName("VetAnimalColorsDef_pkey");

            entity.Property(e => e.RecId)
                    .HasColumnName("RecId")
                    .UseIdentityColumn();
        }
    }
}
