using AutoMapper;
using BrewCloud.Gym.Application.Models;
using BrewCloud.Gym.Domain.Entities;

namespace BrewCloud.Gym.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GymPersonnel, GymPersonnelDto>().ReverseMap();
        }
    }
}