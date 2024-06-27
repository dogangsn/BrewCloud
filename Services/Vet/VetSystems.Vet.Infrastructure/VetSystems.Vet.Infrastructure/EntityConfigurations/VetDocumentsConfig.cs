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
    public class VetDocumentsConfig : IEntityTypeConfiguration<VetDocuments>
    {
        public void Configure(EntityTypeBuilder<VetDocuments> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("VetDocuments_pkey");
             
            entity.Property(x => x.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnName("recid")
                .UseIdentityColumn(1, 1);
        }
    }
}
