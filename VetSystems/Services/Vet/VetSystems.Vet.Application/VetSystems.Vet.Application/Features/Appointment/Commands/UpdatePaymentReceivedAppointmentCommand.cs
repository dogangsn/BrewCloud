using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Shared.Service;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{

    public class UpdatePaymentReceivedAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public bool IsPaymentReceived { get; set; }
    }

    public class UpdatePaymentReceivedAppointmentCommandHandler : IRequestHandler<UpdatePaymentReceivedAppointmentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompletedAppointmentCommandHandler> _logger;
        private readonly IRepository<VetAppointments> _appointmentRepository;

        public UpdatePaymentReceivedAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCompletedAppointmentCommandHandler> logger, IRepository<VetAppointments> appointmentRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(UpdatePaymentReceivedAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                VetAppointments appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                if (appointment == null)
                {
                    _logger.LogWarning($"Not Foun number: {request.Id}");
                    return Response<bool>.Fail("Appointments update failed", 404);
                }


                appointment.IsPaymentReceived = request.IsPaymentReceived;
                _appointmentRepository.Update(appointment);

            }
            catch (Exception ex)
            {
                response.ResponseType = ResponseType.Error;
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
