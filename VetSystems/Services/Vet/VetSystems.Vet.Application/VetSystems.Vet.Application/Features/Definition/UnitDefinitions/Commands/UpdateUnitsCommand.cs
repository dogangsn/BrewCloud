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
    public class UpdateUnitsCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
    }

    public class UpdateUnitsCommandHandler : IRequestHandler<UpdateUnitsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUnitsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.Units> _unitsRepository;

        public UpdateUnitsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateUnitsCommandHandler> logger, IRepository<Domain.Entities.Units> unitsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitsRepository = unitsRepository ?? throw new ArgumentNullException(nameof(unitsRepository));
        }

        public async Task<Response<bool>> Handle(UpdateUnitsCommand request, CancellationToken cancellationToken)
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
                    _logger.LogWarning($"Units update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Store update failed", 404);
                }
                units.UnitCode = request.UnitCode;
                units.UnitName = request.UnitName;
                units.UpdateDate = DateTime.Now;
                units.UpdateUsers = _identity.Account.UserName;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
