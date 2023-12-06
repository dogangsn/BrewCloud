using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Mail.Application.Features.SmtpSettings.Commands;
using VetSystems.Mail.Application.Models.SmtpSettings;
using VetSystems.Mail.Domain.Entities;

namespace VetSystems.Mail.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SmtpSetting, CreateSmtpSettingCommand>().ReverseMap();
            CreateMap<SmtpSetting, SmtpSettingsDto>().ReverseMap();
        }
    }
}
