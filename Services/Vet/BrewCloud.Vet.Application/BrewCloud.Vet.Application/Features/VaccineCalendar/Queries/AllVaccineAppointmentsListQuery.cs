using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Features.Vaccine.Commands;
using BrewCloud.Vet.Application.Models.Appointments;
using BrewCloud.Vet.Application.Models.Definition.Taxis;
using BrewCloud.Vet.Application.Models.Vaccine;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Vaccine.Queries
{
    public class AllVaccineAppointmentsListQuery : IRequest<Response<List<AppointmentsListDto>>>
    {
    }
    public class AllVaccineAppointmentsListQueryHandler : IRequestHandler<AllVaccineAppointmentsListQuery, Response<List<AppointmentsListDto>>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<AllVaccineAppointmentsListQueryHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineCalendar> _vetVaccineCalendarRepository;
        private readonly IMediator _mediator;

        public AllVaccineAppointmentsListQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<AllVaccineAppointmentsListQueryHandler> logger, IRepository<VetVaccineCalendar> vetVaccineCalendarRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetVaccineCalendarRepository = vetVaccineCalendarRepository;
            _mediator = mediator;
        }

        public async Task<Response<List<AppointmentsListDto>>> Handle(AllVaccineAppointmentsListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<AppointmentsListDto>>.Success(200);
            try
            {
                string query = "select vaccinename as text,vaccinedate as startDate, DATEADD(minute, 15, vaccinedate) AS endDate from vetvaccinecalendar where isAdd = 1 and deleted = 0";

                var _data = _uow.Query<AppointmentsListDto>(query).ToList();
                response = new Response<List<AppointmentsListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
