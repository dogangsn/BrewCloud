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

namespace VetSystems.Vet.Application.Features.Store.Commands
{
    public class CreateStoreCommand : IRequest<Response<bool>>
    {
        public bool Active { get; set; } = true;
        public string DepotCode { get; set; } = string.Empty;
        public string DepotName { get; set; } = string.Empty;
    }

    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetStores> _storesRepository;

        public CreateStoreCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetStores> storesRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _storesRepository = storesRepository ?? throw new ArgumentNullException(nameof(storesRepository));
        }

        public async Task<Response<bool>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetStores stores = new()
                {
                    Id = Guid.NewGuid(),
                    DepotCode = request.DepotCode,
                    DepotName = request.DepotName,
                    CreateDate = DateTime.Now,
                    Active = request.Active,
                    CreateUsers = _identity.Account.UserName
                };
                await _storesRepository.AddAsync(stores);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }

            return response;

        }
    }
}
