using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Application.Models.Settings;
using BrewCloud.Account.Domain.Entities;

namespace BrewCloud.Account.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Rolesetting, RoleSettingDto>().ReverseMap();
            CreateMap<RoleSettingDetail, RoleSettingDetailDto>().ReverseMap();
            CreateMap<TitleDefinitions, TitleDefinationDto>().ReverseMap();

        }
    }
}
