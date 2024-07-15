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
        private readonly IRepository<Vet.Domain.Entities.VetVaccine> _vaccineReposiory;

        public UpdateAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateAppointmentCommandHandler> logger, IRepository<Domain.Entities.VetAppointments> appointmentRepository, IRepository<VetVaccine> vaccineReposiory)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _vaccineReposiory = vaccineReposiory;
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
                VetVaccine vetVaccine = new VetVaccine();

                if (request.AppointmentType == (int)AppointmentType.AsiRandevusu)
                {
                    foreach (var item in request.VaccineItems)
                    {
                        vetVaccine = _vaccineReposiory.Get(p=>p.Id == item.ProductId).FirstOrDefault();
                    }

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
                    appointment.VaccineId = vetVaccine.Id == null ? Guid.Empty : vetVaccine.Id;
                    appointment.AppointmentType = request.AppointmentType;
                    appointment.UpdateDate = DateTime.Now;
                    appointment.UpdateUsers = _identity.Account.UserName;
                    appointment.Status = request.Status == null ? appointment.Status : (StatusType)request.Status;
                    appointment.PatientsId = string.IsNullOrEmpty(request.PatientId) ? appointment.PatientsId : Guid.Parse(request.PatientId);
                    await _uow.SaveChangesAsync(cancellationToken);
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
                    appointment.DoctorId = request.DoctorId;
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
