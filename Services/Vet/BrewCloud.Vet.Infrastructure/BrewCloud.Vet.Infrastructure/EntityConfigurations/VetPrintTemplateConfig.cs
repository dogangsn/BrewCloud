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
    public class VetPrintTemplateConfig : IEntityTypeConfiguration<VetPrintTemplate>
    {
        public void Configure(EntityTypeBuilder<VetPrintTemplate> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("VetPrintTemplate_pkey");

            entity.Property(x => x.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("recid")
                .UseIdentityColumn(1, 1);
        }
    }
}
