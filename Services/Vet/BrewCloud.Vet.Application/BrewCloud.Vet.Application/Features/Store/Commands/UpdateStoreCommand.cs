using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Features.Customers.Commands;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Store.Commands
{
    public class UpdateStoreCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public bool Active { get; set; } = true;
        public string DepotCode { get; set; } = string.Empty;
        public string DepotName { get; set; } = string.Empty;
    }

    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStoreCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetStores> _storesRepository;

        public UpdateStoreCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateStoreCommandHandler> logger, IRepository<Domain.Entities.VetStores> storesRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _storesRepository = storesRepository ?? throw new ArgumentNullException(nameof(storesRepository));
        }

        public async Task<Response<bool>> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var stores = await _storesRepository.GetByIdAsync(request.Id);
                if (stores == null)
                {
                    _logger.LogWarning($"Store update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Store update failed", 404);
                }

                stores.Active = request.Active;
                stores.DepotName = request.DepotName;
                stores.DepotCode = request.DepotCode;
                stores.UpdateDate = DateTime.Now;
                stores.UpdateUsers = _identity.Account.UserName;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
