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
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.SaleBuy.Commands
{
    public class UpdateSaleBuyCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class UpdateSaleBuyCommandHandler : IRequestHandler<UpdateSaleBuyCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductDescriptionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _salebuyOwnerRepository;
        private readonly IIdentityRepository _identityRepository;

        public UpdateSaleBuyCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateProductDescriptionCommandHandler> logger, IRepository<Domain.Entities.VetSaleBuyOwner> salebuyOwnerRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _salebuyOwnerRepository = salebuyOwnerRepository ?? throw new ArgumentNullException(nameof(salebuyOwnerRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(UpdateSaleBuyCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

            try
            {
                var salebuyOwner = await _salebuyOwnerRepository.GetByIdAsync(request.Id);
                if (salebuyOwner == null)
                {
                    _logger.LogWarning($"SaleBuyOwner update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }


            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }
}
