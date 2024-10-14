using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Domain.Entities;

namespace BrewCloud.Mail.Infrastructure.EntityConfigurations
{
    public class SmtpSettingConfig : IEntityTypeConfiguration<SmtpSetting>
    {
        public void Configure(EntityTypeBuilder<SmtpSetting> entity)
        {
            entity.HasKey(e => e.Id)
          .HasName("smtpsetting_pkey");
        }
    }
}
