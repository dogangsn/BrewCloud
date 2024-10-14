using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Infrastructure.EntityConfigurations
{
    public class VetMessageLogsConfig : IEntityTypeConfiguration<VetMessageLogs>
    {
        public void Configure(EntityTypeBuilder<VetMessageLogs> entity)
        {
            entity.HasKey(e => e.Id)
                  .HasName("VetMessageLogs_pkey");

   
            entity.Property(x => x.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("recid")
                .UseIdentityColumn(1, 1);
        }
    }
}
