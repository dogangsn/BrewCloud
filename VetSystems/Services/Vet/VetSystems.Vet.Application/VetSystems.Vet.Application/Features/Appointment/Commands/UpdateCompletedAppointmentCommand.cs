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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class UpdateCompletedAppointmentCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class UpdateCompletedAppointmentCommandHandler : IRequestHandler<UpdateCompletedAppointmentCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompletedAppointmentCommandHandler> _logger;
        private readonly IRepository<VetAppointments> _appointmentRepository;

        public UpdateCompletedAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCompletedAppointmentCommandHandler> logger, IRepository<VetAppointments> appointmentRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<string>> Handle(UpdateCompletedAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                Data = string.Empty,
                IsSuccessful = true
            };
            try
            {
                VetAppointments appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                if (appointment == null)
                {
                    _logger.LogWarning($"Not Foun number: {request.Id}");
                    return Response<string>.Fail("Appointments update failed", 404);
                }
                if (appointment.IsPaymentReceived.GetValueOrDefault())
                {
                    return Response<string>.Fail("Tahsilatı Yapılmış İşlemlerde Değişiklik Yapılamaz.", 404);
                }

                appointment.IsCompleted = request.IsCompleted;
                await _uow.SaveChangesAsync(cancellationToken);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
