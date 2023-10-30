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
    public class VetProductMovementsConfig : IEntityTypeConfiguration<VetProductMovements>
    {
        public void Configure(EntityTypeBuilder<VetProductMovements> entity)
        {
            entity.HasKey(e => e.RecId)
         .HasName("VetProductMovements_pkey");

        }
    }
}
