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
    public class UpdateCompletedAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class UpdateCompletedAppointmentCommandHandler : IRequestHandler<UpdateCompletedAppointmentCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(UpdateCompletedAppointmentCommand request, CancellationToken cancellationToken)
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
                appointment.IsCompleted = request.IsCompleted;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}
