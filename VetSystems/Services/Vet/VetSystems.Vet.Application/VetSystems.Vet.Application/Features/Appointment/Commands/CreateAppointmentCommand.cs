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
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class CreateAppointmentCommand : IRequest<Response<bool>>
    {
        public AppointmentsDto CreateAppointments { get; set; }
    }

    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAppointmentHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _AppointmentRepository;

        public CreateAppointmentHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAppointmentHandler> logger, IRepository<Domain.Entities.VetAppointments> AppointmentRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _AppointmentRepository = AppointmentRepository ?? throw new ArgumentNullException(nameof(AppointmentRepository));
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

                Vet.Domain.Entities.VetAppointments Appointments = new()
                {
                    RecId = Guid.NewGuid(),
                    BeginDate = request.CreateAppointments.beginDate,
                    EndDate = request.CreateAppointments.endDate,
                    CustomerId = request.CreateAppointments.customerId,
                    DoctorId = request.CreateAppointments.doctorId,
                    Note= request.CreateAppointments.note,
                    
                    Deleted = false,
                    CreateDate = DateTime.UtcNow,
                };

                await _AppointmentRepository.AddAsync(Appointments);
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
