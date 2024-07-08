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
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Application.Models.Customers;
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
        private readonly IRepository<Vet.Domain.Entities.VetPatients> _vetPatientsRepository;
        private readonly IMediator _mediator;

        public CreateVaccineExaminationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateVaccineExaminationCommandHandler> logger, IRepository<VetVaccineCalendar> vetVaccineCalendarRepository, IMediator mediator, IRepository<VetPatients> vetPatientsRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetVaccineCalendarRepository = vetVaccineCalendarRepository;
            _mediator = mediator;
            _vetPatientsRepository = vetPatientsRepository;
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
                VetPatients patient = _vetPatientsRepository.Get(p => p.Id == request.VaccineCalendars[0].PatientId).FirstOrDefault();
                PatientsDetailsDto patientsDetails = new()
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    BirthDate = patient.BirthDate.ToString("yyyy-MM-dd"),
                    ChipNumber = patient.ChipNumber ?? string.Empty,
                    Sex = patient.Sex,
                    AnimalType = patient.AnimalType,
                    AnimalBreed = patient.AnimalBreed,
                    AnimalColor = patient.AnimalColor,
                    ReportNumber = patient.ReportNumber ?? string.Empty,
                    SpecialNote = patient.SpecialNote ?? string.Empty,
                    Sterilization = patient.Sterilization,
                    Active = patient.Active ?? true,
                    IsVaccineCalendarCreate = true,
                };
                UpdatePatientCommand updatePatient = new UpdatePatientCommand();
                updatePatient.PatientDetails = patientsDetails;
                updatePatient.CustomerId = request.VaccineCalendars.First().CustomerId;
                var resp = await _mediator.Send(updatePatient);

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
