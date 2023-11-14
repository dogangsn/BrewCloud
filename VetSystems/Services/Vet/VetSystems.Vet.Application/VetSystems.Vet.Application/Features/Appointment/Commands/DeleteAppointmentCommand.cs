﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Appointment.Commands;
using VetSystems.Vet.Application.Features.Store.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class DeleteAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAppointmentCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _appointmentsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteAppointmentCommandHandler> logger, IRepository<Domain.Entities.VetAppointments> appointmentRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appointmentsRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var stores = await _appointmentsRepository.GetByIdAsync(request.Id);
                if (stores == null)
                {
                    _logger.LogWarning($"Appointment update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Appointment update failed", 404);
                }

                stores.Deleted = true;
                stores.DeletedDate = DateTime.Now;
                stores.DeletedUsers = _identityRepository.Account.UserName;




                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }


}
