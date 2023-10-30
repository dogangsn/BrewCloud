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

namespace VetSystems.Vet.Application.Features.Demands.DemandProducts.Commands
{
    public class DeleteDemandProductsCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDemandProductsCommandHandler : IRequestHandler<DeleteDemandProductsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDemandProductsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetDemandProducts> _demandProductsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteDemandProductsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteDemandProductsCommandHandler> logger, IRepository<Domain.Entities.VetDemandProducts> demandProductsRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandProductsRepository = demandProductsRepository ?? throw new ArgumentNullException(nameof(demandProductsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteDemandProductsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var demandProducts = await _demandProductsRepository.GetByIdAsync(request.Id);
                if (demandProducts == null)
                {
                    _logger.LogWarning($"DemandProduct update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                demandProducts.Deleted = true;
                demandProducts.DeletedDate = DateTime.Now;
                demandProducts.DeletedUsers = _identityRepository.Account.UserName;
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
