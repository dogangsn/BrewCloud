﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Infrastructure.EntityConfigurations
{
    public class VetLogsConfig : IEntityTypeConfiguration<VetLogs>
    {
        public void Configure(EntityTypeBuilder<VetLogs> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("VetLogs_pkey");
        }
    }
}
