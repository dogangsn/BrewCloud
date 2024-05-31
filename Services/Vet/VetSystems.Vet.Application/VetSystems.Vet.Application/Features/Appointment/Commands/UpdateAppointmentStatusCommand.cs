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
    public class UpdateAppointmentStatusCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }

    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppointmentStatusCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _appointmentRepository;

        public UpdateAppointmentStatusCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateAppointmentStatusCommandHandler> logger, IRepository<VetAppointments> appointmentRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                if (appointment == null || appointment.Deleted)
                {
                    return Response<bool>.Fail("Not Found Record", 400);
                }

                appointment.Status = (StatusType)request.Status;
                appointment.UpdateUsers = _identity.Account.UserName;
                appointment.UpdateDate = DateTime.Now;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
