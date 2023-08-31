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
    public class SaleBuyTrans : IEntityTypeConfiguration<VetSaleBuyTrans>
    {
        public void Configure(EntityTypeBuilder<VetSaleBuyTrans> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("VetSaleBuyTrans_pkey");

            //entity.Property<decimal>("Amount")
            // .HasPrecision(18, 2)
            // .HasColumnType("numeric(18,2)")
            // .HasColumnName("Amount")
            // .HasDefaultValue(0.0);
        }
    }
}
