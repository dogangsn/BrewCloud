using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Mail.Domain.Entities;

namespace VetSystems.Mail.Infrastructure.EntityConfigurations
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
