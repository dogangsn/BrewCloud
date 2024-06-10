using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    public class SaleBuyTrans : IEntityTypeConfiguration<VetSaleBuyTrans>
    {
        public void Configure(EntityTypeBuilder<VetSaleBuyTrans> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("VetSaleBuyTrans_pkey");


            //entity.Property(e => e.RecId)
            //    .ValueGeneratedOnAdd()
            //    .HasColumnName("recid")
            //    .UseIdentityColumn(1,1);

        }
    }
}
