using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;
using static Dapper.SqlMapper;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    public class VetLabDocumentConfig : IEntityTypeConfiguration<VetLabDocument>
    {
        public void Configure(EntityTypeBuilder<VetLabDocument> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("VetLabDocument_pkey");
        }
    }
}
