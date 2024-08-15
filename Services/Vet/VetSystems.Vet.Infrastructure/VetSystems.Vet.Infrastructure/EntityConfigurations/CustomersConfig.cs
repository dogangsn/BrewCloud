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
    public class CustomersConfig : IEntityTypeConfiguration<VetCustomers>
    {
        public void Configure(EntityTypeBuilder<VetCustomers> entity)
        {
            entity.HasKey(e => e.Id)
                           .HasName("VetCustomers_pkey");

            entity.Property(x => x.RecId)
            .ValueGeneratedOnAdd()
            .HasColumnName("recid")
            .UseIdentityColumn(1000, 1);
        }
    }
}
