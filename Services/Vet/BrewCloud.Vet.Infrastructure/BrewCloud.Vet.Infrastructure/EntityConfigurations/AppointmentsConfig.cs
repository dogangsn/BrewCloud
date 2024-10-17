﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Infrastructure.EntityConfigurations
{
    public class AppointmentsConfig : IEntityTypeConfiguration<VetAppointments>
    {
        public void Configure(EntityTypeBuilder<VetAppointments> entity)
        {
            entity.HasKey(e => e.Id)
                      .HasName("VetAppointments_pkey");
        }
    }
}