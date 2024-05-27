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
                appointment.VaccineId = request.VaccineId;
                appointment.AppointmentType = request.AppointmentType;
                appointment.UpdateDate = DateTime.Now;
                appointment.UpdateUsers = _identity.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);

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
