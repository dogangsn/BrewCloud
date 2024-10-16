﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Appointments;
using BrewCloud.Vet.Application.Models.Definition.AppointmentTypes;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.AppointmentTypes.Queries
{
    public class GetAppointmentTypesListQuery : IRequest<Response<List<AppointmentTypesDto>>>
    {
    }

    public class GetAppointmentTypesListQueryHandler : IRequestHandler<GetAppointmentTypesListQuery, Response<List<AppointmentTypesDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetAppointmentTypes> _AppointmentTypesRepository;

        public GetAppointmentTypesListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetAppointmentTypes> appointmentTypesRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _AppointmentTypesRepository = appointmentTypesRepository;
        }

        public async Task<Response<List<AppointmentTypesDto>>> Handle(GetAppointmentTypesListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<AppointmentTypesDto>>.Success(200);
            try
            {
                List<VetAppointmentTypes> appointmentsList = (await _AppointmentTypesRepository.GetAsync(x => x.Deleted == false)).OrderBy(x => x.Type).ToList();
                var result = _mapper.Map<List<AppointmentTypesDto>>(appointmentsList);

                response.Data = result;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
