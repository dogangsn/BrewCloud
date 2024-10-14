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
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.ProductDescription.Commands
{
    public class UpdateProductActiveCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public bool? Active { get; set; } = true;
    }

    public class UpdateProductActiveCommandHandler : IRequestHandler<UpdateProductActiveCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductActiveCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public UpdateProductActiveCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateProductActiveCommandHandler> logger, IRepository<VetProducts> productRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Response<bool>> Handle(UpdateProductActiveCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    _logger.LogWarning($"Product update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                product.Active = request.Active;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
