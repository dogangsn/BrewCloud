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
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Definition.UnitDefinitions.Commands
{
    public class CreateUnitsCommand : IRequest<Response<bool>>
    {
        public string UnitCode { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
    }

    public class CreateUnitsCommandHandler : IRequestHandler<CreateUnitsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetUnits> _unitsRepositoryy;

        public CreateUnitsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetUnits> unitsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitsRepositoryy = unitsRepository ?? throw new ArgumentNullException(nameof(unitsRepository));
        }
        public async Task<Response<bool>> Handle(CreateUnitsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            try
            {
                VetUnits units = new()
                {
                    Id = Guid.NewGuid(),
                    UnitCode = request.UnitCode,
                    UnitName = request.UnitName,
                    CreateDate = DateTime.Now
                };
                await _unitsRepositoryy.AddAsync(units);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}
