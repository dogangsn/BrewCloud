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
    internal class ProductCategoriesConfig : IEntityTypeConfiguration<ProductCategories>
    {
        public void Configure(EntityTypeBuilder<ProductCategories> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("ProductCategories_pkey");
        }
    }
}
