using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    public class AnimalsTypeConfig : IEntityTypeConfiguration<AnimalsType>
    {
        public void Configure(EntityTypeBuilder<AnimalsType> entity)
        {
            entity.HasKey(e => e.Id)
                    .HasName("AnimalsType_pkey");

            entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .UseIdentityColumn();

        }
    }
}
