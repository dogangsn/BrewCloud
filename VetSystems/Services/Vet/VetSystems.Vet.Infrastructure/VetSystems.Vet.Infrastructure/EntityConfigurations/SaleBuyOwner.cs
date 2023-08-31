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
    public class SaleBuyOwner : IEntityTypeConfiguration<VetSaleBuyOwner>
    {
        public void Configure(EntityTypeBuilder<VetSaleBuyOwner> entity)
        {
            entity.HasKey(e => e.Id)
                  .HasName("VetSaleBuyOwner_pkey");

            //entity.Property<decimal>("Total")
            // .HasPrecision(18, 2)
            // .HasColumnType("numeric(18,2)")
            // .HasColumnName("Total")
            // .HasDefaultValue(0.0);

        }
    }
}
