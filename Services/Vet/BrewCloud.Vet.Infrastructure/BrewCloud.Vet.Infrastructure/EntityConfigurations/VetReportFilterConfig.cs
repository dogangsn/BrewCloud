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

    public class VetReportFilterConfig : IEntityTypeConfiguration<VetReportFilter>
    {
        public void Configure(EntityTypeBuilder<VetReportFilter> entity)
        {
            entity.HasKey(e => e.RecId)
         .HasName("VetReportFilter_pkey");

        }
    }
}
