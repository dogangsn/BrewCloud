﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Infrastructure.EntityConfigurations
{
    public class VetVaccineConfig : IEntityTypeConfiguration<VetVaccine>
    {
        public void Configure(EntityTypeBuilder<VetVaccine> entity)
        {
            entity.HasKey(e => e.Id)
.                   HasName("VetVaccine_pkey");
        }
    }
}
