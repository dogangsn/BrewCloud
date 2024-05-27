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
    public class DeleteStoreCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStoreCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetStores> _storesRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteStoreCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteStoreCommandHandler> logger, IRepository<Domain.Entities.VetStores> storesRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _storesRepository = storesRepository ?? throw new ArgumentNullException(nameof(storesRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
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
                    return Response<bool>.Fail("Property update failed", 404);
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
