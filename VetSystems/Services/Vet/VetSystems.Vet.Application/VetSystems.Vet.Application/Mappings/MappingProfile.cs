﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Application.Models.Definition.AppointmentTypes;
using VetSystems.Vet.Application.Models.Definition.Taxis;
using VetSystems.Vet.Application.Models.Settings.SmsParameters;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<VetAppointments, AppointmentsDto>().ReverseMap();
            CreateMap<VetSmsParameters, SmsParametersDto>().ReverseMap();
            CreateMap<VetAppointmentTypes, AppointmentTypesDto>().ReverseMap();
            CreateMap<VetTaxis, TaxisDto>().ReverseMap();
        
        }
    }
}
