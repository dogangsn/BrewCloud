﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;
using static Dapper.SqlMapper;

namespace BrewCloud.Vet.Infrastructure.EntityConfigurations
{
    public class VetAccomodationConfig : IEntityTypeConfiguration<VetAccomodation>
    {
        public void Configure(EntityTypeBuilder<VetAccomodation> entity)
        {
            entity.HasKey(e => e.Id)
               .HasName("VetAccomodation_pkey");
        }
    }
}
