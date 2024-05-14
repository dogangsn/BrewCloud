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
    public class StockTrackingConfig : IEntityTypeConfiguration<VetStockTracking>
    {
        public void Configure(EntityTypeBuilder<VetStockTracking> entity)
        {
            entity.HasKey(e => e.Id)
                    .HasName("VetStockTracking_pkey");
        }
    }
}
