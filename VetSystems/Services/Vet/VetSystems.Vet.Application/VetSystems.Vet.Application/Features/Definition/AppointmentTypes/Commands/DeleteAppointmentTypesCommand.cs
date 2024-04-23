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

namespace VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Commands
{
    public class DeleteAppointmentTypesCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAppointmentTypesCommandHandler : IRequestHandler<DeleteAppointmentTypesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAppointmentTypesCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointmentTypes> _appointmentTypesRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteAppointmentTypesCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteAppointmentTypesCommandHandler> logger, IRepository<VetAppointmentTypes> appointmentTypesRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appointmentTypesRepository = appointmentTypesRepository;
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteAppointmentTypesCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var appointmentTypes = await _appointmentTypesRepository.GetByIdAsync(request.Id);
                if (appointmentTypes == null)
                {
                    _logger.LogWarning($"taxis deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                int[] inculedType = { 1, 2, 3, 4, 5, 6 };
                if (inculedType.Contains(appointmentTypes.Type))
                {
                    return Response<bool>.Fail("Varsayılan Randevu Tipleri Silinemez", 404);
                }

                appointmentTypes.Deleted = true;
                appointmentTypes.DeletedDate = DateTime.Now;
                appointmentTypes.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
