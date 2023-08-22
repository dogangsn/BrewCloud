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

namespace VetSystems.Vet.Application.Features.Definition.UnitDefinitions.Commands
{
    public class DeleteUnitsCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUnitsCommandHandler : IRequestHandler<DeleteUnitsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUnitsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.Units> _unitsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteUnitsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteUnitsCommandHandler> logger, IRepository<Domain.Entities.Units> unitsRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitsRepository = unitsRepository ?? throw new ArgumentNullException(nameof(unitsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteUnitsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var units = await _unitsRepository.GetByIdAsync(request.Id);
                if (units == null)
                {
                    _logger.LogWarning($"Untis update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                units.Deleted = true;
                units.DeletedDate = DateTime.Now;
                units.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
