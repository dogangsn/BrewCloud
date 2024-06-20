using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Vaccine;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.VaccineCalendar.Commands
{
    public class CreateVaccineExaminationCommand : IRequest<Response<string>>
    {
        public List<VetVaccineCalendar> VaccineCalendars { get; set; }
    }

    public class CreateVaccineExaminationCommandHandler : IRequestHandler<CreateVaccineExaminationCommand, Response<string>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVaccineExaminationCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineCalendar> _vetVaccineCalendarRepository;
        private readonly IMediator _mediator;

        public CreateVaccineExaminationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateVaccineExaminationCommandHandler> logger, IRepository<VetVaccineCalendar> vetVaccineCalendarRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetVaccineCalendarRepository = vetVaccineCalendarRepository;
            _mediator = mediator;
        }

        public async Task<Response<string>> Handle(CreateVaccineExaminationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = string.Empty
            };

            if (request.VaccineCalendars == null || !request.VaccineCalendars.Any())
            {
                response.ResponseType = ResponseType.Error;
                response.IsSuccessful = false;
                return response;
            }

            _uow.CreateTransaction(IsolationLevel.ReadCommitted);
            try
            {
                List<VetVaccineCalendar> vetVaccineCalendars = request.VaccineCalendars;
                foreach (var vaccineCalendar in vetVaccineCalendars)
                {
                    await _vetVaccineCalendarRepository.AddAsync(vaccineCalendar);
                }

                await _uow.SaveChangesAsync(cancellationToken);

                _uow.Commit();

                response.Data = string.Join(",", request.VaccineCalendars.Select(vc => vc.Id.ToString()));
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.ResponseType = ResponseType.Error;
                response.IsSuccessful = false;
                _logger.LogError(ex, "Error occurred while creating vaccine examinations");
            }
            return response;
        }
    }
}
