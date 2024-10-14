using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Application.Features.SmtpMails.Commands;
using BrewCloud.Mail.Application.Features.SmtpSettings.Commands;
using BrewCloud.Mail.Application.Models.SmtpSettings;
using BrewCloud.Mail.Domain.Entities;
using BrewCloud.Shared.Dtos.MailKit;

namespace BrewCloud.Mail.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SmtpSetting, CreateSmtpSettingCommand>().ReverseMap();
            CreateMap<SmtpSetting, SmtpSettingsDto>().ReverseMap();
            CreateMap<MailDetailDto, SendMailCommand>().ReverseMap();
            CreateMap<MailSenderDto, SmtpSetting>().ReverseMap();
            //CreateMap<SmtpMail, MailDetailDto>().ReverseMap();
        }
    }
}
