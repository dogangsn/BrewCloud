using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;


namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Products>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Products> entity)
        {
            entity.HasKey(e => e.Id)
                            .HasName("products_pkey");
        }
    }
}
