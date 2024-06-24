using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Vaccine.Commands;
using VetSystems.Vet.Application.Models.Definition.Taxis;
using VetSystems.Vet.Application.Models.Vaccine;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Vaccine.Queries
{
    public class PatientVaccineListQuery : IRequest<Response<List<VetVaccineCalendar>>>
    {
        public Guid? Id { get; set; }
        public Guid? PatientId { get; set; }
    }
    public class PatientVaccineListQueryHandler : IRequestHandler<PatientVaccineListQuery, Response<List<VetVaccineCalendar>>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientVaccineListQueryHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineCalendar> _vetVaccineCalendarRepository;
        private readonly IMediator _mediator;

        public PatientVaccineListQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<PatientVaccineListQueryHandler> logger, IRepository<VetVaccineCalendar> vetVaccineCalendarRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetVaccineCalendarRepository = vetVaccineCalendarRepository;
            _mediator = mediator;
        }

        public async Task<Response<List<VetVaccineCalendar>>> Handle(PatientVaccineListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<VetVaccineCalendar>>.Success(200);
            List<VetVaccineCalendar> _vaccine = new List<VetVaccineCalendar>();
            try
            {
                if (!String.IsNullOrEmpty(request.PatientId.ToString()))
                {
                    _vaccine = (await _vetVaccineCalendarRepository.GetAsync(x => x.Deleted == false && x.PatientId == request.PatientId && x.IsAdd == true)).ToList();                    
                }
                else if (!String.IsNullOrEmpty(request.Id.ToString()))
                {
                    _vaccine = (await _vetVaccineCalendarRepository.GetAsync(x => x.Deleted == false && x.Id == request.Id)).ToList();
                }
                else
                {
                    _vaccine = (await _vetVaccineCalendarRepository.GetAsync(x => x.Deleted == false && x.IsAdd == true)).ToList();
                }

                var result = _mapper.Map<List<VetVaccineCalendar>>(_vaccine.OrderByDescending(e => e.VaccineDate));
                response.Data = result;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
