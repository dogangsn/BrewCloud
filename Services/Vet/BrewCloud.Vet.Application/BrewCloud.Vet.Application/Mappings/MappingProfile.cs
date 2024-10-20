﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Application.Models.Appointments;
using BrewCloud.Vet.Application.Models.Definition.AppointmentTypes;
using BrewCloud.Vet.Application.Models.Definition.PrintTemplate;
using BrewCloud.Vet.Application.Models.Definition.Taxis;
using BrewCloud.Vet.Application.Models.Lab;
using BrewCloud.Vet.Application.Models.Settings.SmsParameters;

using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<VetAppointments, AppointmentsDto>().ReverseMap();
            CreateMap<VetSmsParameters, SmsParametersDto>().ReverseMap();
            CreateMap<VetAppointmentTypes, AppointmentTypesDto>().ReverseMap();
            CreateMap<VetTaxis, TaxisDto>().ReverseMap();
            CreateMap<VetVaccine, Models.Vaccine.VaccineListDto>().ReverseMap();
            CreateMap<VetVaccineMedicine, Models.Vaccine.VetVaccineMedicineListDto>().ReverseMap();
            CreateMap<VetRooms, Models.PetHotels.Rooms.RoomListDto>().ReverseMap();
            CreateMap<VetLogs, LogDto>().ReverseMap();
            CreateMap<VetPrintTemplate, PrintTemplateListDto>().ReverseMap();
            CreateMap<VetLabDocument, LabDocumentDto>().ReverseMap();
        }
    }
}
