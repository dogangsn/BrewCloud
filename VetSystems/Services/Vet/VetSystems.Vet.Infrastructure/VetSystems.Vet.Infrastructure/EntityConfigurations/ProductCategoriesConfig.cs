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
    internal class ProductCategoriesConfig : IEntityTypeConfiguration<VetProductCategories>
    {
        public void Configure(EntityTypeBuilder<VetProductCategories> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetProductCategories_pkey");
        }
    }
}
