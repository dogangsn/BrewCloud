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
    public class CustomerGroupDefConfig : IEntityTypeConfiguration<VetCustomerGroupDef>
    {
        public void Configure(EntityTypeBuilder<VetCustomerGroupDef> entity)
        {
            entity.HasKey(e => e.Id)
            .HasName("VetCustomerGroupDef_pkey");

        }
    }
}
