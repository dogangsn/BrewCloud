using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Gym.Domain.Entities;

namespace BrewCloud.Gym.Infrastructure.EntityConfigurations
{
    public class GymLogsConfig : IEntityTypeConfiguration<GymLogs>
    {
        public void Configure(EntityTypeBuilder<GymLogs> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("GymLogs_pkey");
        }
    }
}
