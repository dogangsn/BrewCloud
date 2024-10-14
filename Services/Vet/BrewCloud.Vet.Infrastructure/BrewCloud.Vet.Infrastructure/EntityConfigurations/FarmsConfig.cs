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
    public class FarmsConfig : IEntityTypeConfiguration<VetFarms>
    {
        public void Configure(EntityTypeBuilder<VetFarms> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("VetFarms_pkey");

            entity.Property(x => x.RecId)
           .ValueGeneratedOnAdd()
           .HasColumnName("recid")
           .UseIdentityColumn(1, 1);
        }
    }
}
