﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    internal class AgendaConfig : IEntityTypeConfiguration<VetAgenda>
    {
        public void Configure(EntityTypeBuilder<VetAgenda> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetAgenda_pkey");
        }
    }
}
