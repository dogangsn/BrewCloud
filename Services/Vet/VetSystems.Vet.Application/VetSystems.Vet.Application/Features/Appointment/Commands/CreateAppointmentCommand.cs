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
using VetSystems.Shared.Events;
using VetSystems.Shared.HubService;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.VaccineCalendar.Commands;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Application.Services.Hub;
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
        private readonly IHubService _hubService;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customerRepository;

        public CreateAppointmentHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAppointmentHandler> logger, IRepository<Domain.Entities.VetAppointments> AppointmentRepository, IRepository<Vet.Domain.Entities.VetPatients> PatientRepository, IRepository<VetVaccine> vaccineRepository, IHubService hubService, IRepository<VetVaccineCalendar> vaccineCalendarRepository, IRepository<VetCustomers> customerRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _AppointmentRepository = AppointmentRepository ?? throw new ArgumentNullException(nameof(AppointmentRepository));
            _PatientRepository = PatientRepository ?? throw new ArgumentNullException(nameof(PatientRepository));
            _vaccineRepository = vaccineRepository ?? throw new ArgumentNullException(nameof(vaccineRepository));
            _hubService = hubService;
            _vaccineCalendarRepository = vaccineCalendarRepository;
            _customerRepository = customerRepository;
            _vaccineCalendarRepository = vaccineCalendarRepository ?? throw new ArgumentNullException(nameof(vaccineCalendarRepository));
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
                List<AppointmentCalendarDto> appointments = new List<AppointmentCalendarDto>();
                Guid _id = Guid.NewGuid();
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                VetPatients patient = _PatientRepository.Get(p => p.Id == Guid.Parse(request.PatientId)).FirstOrDefault();

                if (request.AppointmentType == (int)AppointmentType.AsiRandevusu)
                {
                    List<VetVaccineCalendar> vaccineCalendars = new List<VetVaccineCalendar>();
                    foreach (var item in request.VaccineItems)
                    {
                        VetVaccine vetVaccine = _vaccineRepository.Get(p => p.Id == item.ProductId).FirstOrDefault();
                        
                            VetVaccineCalendar vaccineAppointment = new()
                            {
                                RecId = 0,
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

                    VetCustomers customers = await _customerRepository.GetByIdAsync(Guid.Parse(request.CustomerId));

                    Vet.Domain.Entities.VetAppointments Appointments = new()
                    {
                        Id = _id,
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


                    AppointmentCalendarDto dto = new AppointmentCalendarDto
                    {
                        Id = _id,
                        Text = (customers != null ? customers.FirstName + " " + customers.LastName : "") + " " + GetTextResponse(request.AppointmentType),
                        StartDate = TimeZoneInfo.ConvertTimeFromUtc(request.BeginDate, localTimeZone),
                        EndDate = TimeZoneInfo.ConvertTimeFromUtc(request.BeginDate.AddMinutes(10), localTimeZone),
                    };
                    appointments.Add(dto);

                }
                await _uow.SaveChangesAsync(cancellationToken);

                if (request.AppointmentType != (int)AppointmentType.AsiRandevusu)
                    PushHubService(appointments);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;

        }

        public void PushHubService(List<AppointmentCalendarDto> appointments)
        {
            var req = new RefreshAppointmentCalendarRequest
            {
                UserId = _identity.Account.UserId,
                Appointments = appointments
            };
            var result = _hubService.SendRefreshAppointment(req);
        }

        public string GetTextResponse(int appointmentType)
        {
            switch (appointmentType)
            {
                case 0:
                    return "İlk Muayene";
                case 1:
                    return "Aşı Randevusu";
                case 2:
                    return "Genel Muayene";
                case 3:
                    return "Kontrol Muayene";
                case 4:
                    return "Operasyon";
                case 5:
                    return "Tıraş";
                case 6:
                    return "Tedavi";
                default:
                    return "Diğer";
            }
        }

    }
}
