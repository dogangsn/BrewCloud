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
    public class VetAccomodationCheckOutsConfig : IEntityTypeConfiguration<VetAccomodationCheckOuts>
    {
        public void Configure(EntityTypeBuilder<VetAccomodationCheckOuts> entity)
        {
            entity.HasKey(e => e.Id)
             .HasName("VetAccomodationCheckOuts_pkey");

            entity.Property(x => x.RecId)
             .ValueGeneratedOnAdd()
             .HasColumnName("recid")
             .UseIdentityColumn(1, 1);
        }
    }
}
