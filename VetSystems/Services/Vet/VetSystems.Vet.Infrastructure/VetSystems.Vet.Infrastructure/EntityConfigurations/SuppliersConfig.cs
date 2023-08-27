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
    internal class SuppliersConfig : IEntityTypeConfiguration<VetSuppliers>
    {
        public void Configure(EntityTypeBuilder<VetSuppliers> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetSuppliers_pkey");
        }
    }
}
