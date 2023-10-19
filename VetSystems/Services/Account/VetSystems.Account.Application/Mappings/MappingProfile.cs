using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Application.Models.Settings;
using VetSystems.Account.Domain.Entities;

namespace VetSystems.Account.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Rolesetting, RoleSettingDto>().ReverseMap();
            CreateMap<RoleSettingDetail, RoleSettingDetailDto>().ReverseMap();

        }
    }
}
