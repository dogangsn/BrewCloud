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
    public class DemandTransConfig : IEntityTypeConfiguration<VetDemandTrans>
    {
        public void Configure(EntityTypeBuilder<VetDemandTrans> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetDemandTrans_pkey");
        }
    }
}
