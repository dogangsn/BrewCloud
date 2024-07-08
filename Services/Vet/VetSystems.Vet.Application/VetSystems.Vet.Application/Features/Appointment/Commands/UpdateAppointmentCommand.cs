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
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Shared.Enums;
using VetSystems.Vet.Domain.Entities;
using Grpc.Core;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class UpdateAppointmentCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public DateTime BeginDate { get; set; }
        public string Note { get; set; } = string.Empty;
        public Guid? DoctorId { get; set; }
        public Guid? VaccineId { get; set; }
        public int AppointmentType { get; set; }
        public string? CustomerId { get; set; }
        public int? Status { get; set; }
        public string PatientId { get; set; }
        public List<VaccineListDto>? VaccineItems { get; set; }

    }

    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppointmentCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _appointmentRepository;

        public UpdateAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateAppointmentCommandHandler> logger, IRepository<Domain.Entities.VetAppointments> appointmentRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<string>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                if (request.AppointmentType == (int)AppointmentType.AsiRandevusu)
                {
                    foreach (var item in request.VaccineItems)
                    {

                        var appointment = await _appointmentRepository.GetByIdAsync(item.Id);

                        //Vet.Domain.Entities.VetAppointments Appointments = new()
                        //{
                        //    BeginDate = TimeZoneInfo.ConvertTimeFromUtc(item.Date, localTimeZone),
                        //    EndDate = TimeZoneInfo.ConvertTimeFromUtc(item.Date.AddMinutes(10), localTimeZone),
                        //    CustomerId = Guid.Parse(request.CustomerId),
                        //    DoctorId = Guid.Parse(request.DoctorId),
                        //    Note = request.Note,
                        //    AppointmentType = request.AppointmentType,
                        //    Deleted = false,
                        //    CreateDate = DateTime.UtcNow,
                        //    VaccineId = item.ProductId,
                        //    IsCompleted = item.IsComplated,
                        //    CreateUsers = _identity.Account.UserName,
                        //    Status = (StatusType)request.Status,
                        //    PatientsId = Guid.Parse(request.PatientId)
                        //};
                        //await _AppointmentRepository.AddAsync(Appointments);
                    }
                }
                else
                {
                    var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                    if (appointment == null || appointment.Deleted)
                    {
                        response.IsSuccessful = false;
                        response.ResponseType = ResponseType.Error;
                        response.Data = "Kayıt Bulunamadı.";
                        return response;
                    }
                    appointment.BeginDate = TimeZoneInfo.ConvertTimeFromUtc(request.BeginDate, localTimeZone);
                    appointment.EndDate = TimeZoneInfo.ConvertTimeFromUtc(request.BeginDate.AddMinutes(10), localTimeZone);
                    appointment.Note = request.Note;
                    appointment.VaccineId = request.VaccineId == null ? Guid.Empty : request.VaccineId;
                    appointment.AppointmentType = request.AppointmentType;
                    appointment.UpdateDate = DateTime.Now;
                    appointment.UpdateUsers = _identity.Account.UserName;
                    appointment.Status = request.Status == null ? appointment.Status : (StatusType)request.Status;
                    appointment.PatientsId = string.IsNullOrEmpty(request.PatientId) ? appointment.PatientsId : Guid.Parse(request.PatientId);
                    await _uow.SaveChangesAsync(cancellationToken);
                }
             
            }
            catch (Exception ex)
            {
                response = Response<string>.Fail("appointment kayıt edilemedi: " + ex.Message, 501);
                _logger.LogError("appointment not created: " + ex.Message);
            }
            return response;
        }
    }
}
