using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Enums;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.VaccineCalendar.Commands;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class CreateAppointmentCommand : IRequest<Response<bool>>
    {
        public DateTime BeginDate { get; set; }
        public string Note { get; set; } = string.Empty;
        public string? DoctorId { get; set; }
        public string? CustomerId { get; set; }
        public int AppointmentType { get; set; }
        public int Status { get; set; }
        public string PatientId { get; set; }
        public List<VaccineListDto>? VaccineItems { get; set; } 
    }

    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAppointmentHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _AppointmentRepository;
        private readonly IRepository<Vet.Domain.Entities.VetPatients> _PatientRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccine> _vaccineRepository;
        private readonly IRepository<Vet.Domain.Entities.VetVaccineCalendar> _vaccineCalendarRepository;

        public CreateAppointmentHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAppointmentHandler> logger, IRepository<Domain.Entities.VetAppointments> AppointmentRepository, IRepository<Vet.Domain.Entities.VetPatients> PatientRepository, IRepository<VetVaccine> vaccineRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _AppointmentRepository = AppointmentRepository ?? throw new ArgumentNullException(nameof(AppointmentRepository));
            _PatientRepository = PatientRepository ?? throw new ArgumentNullException(nameof(PatientRepository));
            _vaccineRepository = vaccineRepository ?? throw new ArgumentNullException(nameof(vaccineRepository));
        }

        public async Task<Response<bool>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                VetPatients patient = _PatientRepository.Get(p => p.Id == Guid.Parse(request.PatientId)).FirstOrDefault();

                if (request.AppointmentType == (int)AppointmentType.AsiRandevusu)
                {
                    List<VetVaccineCalendar> vaccineCalendars = new List<VetVaccineCalendar>();
                    foreach (var item in request.VaccineItems)
                    {
                        VetVaccine vetVaccine = _vaccineRepository.Get(p=>p.Id == item.Id).FirstOrDefault();
                        VetVaccineCalendar vaccineAppointment = new()
                        {
                            IsAdd = true,
                            PatientId = Guid.Parse(request.PatientId),
                            CreateUsers = _identity.Account.UserName,
                            VaccineDate = request.BeginDate,
                            IsDone = false,
                            CustomerId = Guid.Parse(request.CustomerId),
                            VaccineId = item.Id,
                            AnimalType = patient.AnimalType.Value,
                            VaccineName = vetVaccine.VaccineName
                        };

                        await _vaccineCalendarRepository.AddAsync(vaccineAppointment);

                        Vet.Domain.Entities.VetAppointments Appointments = new()
                        {
                            BeginDate = TimeZoneInfo.ConvertTimeFromUtc(item.Date, localTimeZone),
                            EndDate = TimeZoneInfo.ConvertTimeFromUtc(item.Date.AddMinutes(10), localTimeZone),
                            CustomerId = Guid.Parse(request.CustomerId),
                            DoctorId = Guid.Parse(request.DoctorId),
                            Note = request.Note,
                            AppointmentType = request.AppointmentType,
                            Deleted = false,
                            CreateDate = DateTime.UtcNow,
                            VaccineId = item.ProductId,
                            IsCompleted = item.IsComplated,
                            CreateUsers = _identity.Account.UserName,
                            Status = (StatusType)request.Status,
                            PatientsId = Guid.Parse(request.PatientId)
                           
                        };

                                                

                        await _AppointmentRepository.AddAsync(Appointments);
                    }
                }
                else
                {
                    Vet.Domain.Entities.VetAppointments Appointments = new()
                    {
                        BeginDate = TimeZoneInfo.ConvertTimeFromUtc(request.BeginDate, localTimeZone),
                        EndDate = TimeZoneInfo.ConvertTimeFromUtc(request.BeginDate.AddMinutes(10), localTimeZone),
                        CustomerId = Guid.Parse(request.CustomerId),
                        DoctorId = Guid.Parse(request.DoctorId),
                        Note = request.Note,
                        AppointmentType = request.AppointmentType,
                        Deleted = false,
                        CreateDate = DateTime.UtcNow,
                        IsCompleted = false,
                        CreateUsers = _identity.Account.UserName,
                        Status = (StatusType)request.Status,
                        PatientsId = Guid.Parse(request.PatientId)
                    };
                    await _AppointmentRepository.AddAsync(Appointments);

                }
                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;

        }
    }
}
