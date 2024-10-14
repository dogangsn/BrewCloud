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
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.ProductCategory.Commands
{
    public class UpdateProductCategoriesCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string CategoryCode { get; set; } = string.Empty;
    }

    public class UpdateProductCategoriesCommandHandler : IRequestHandler<UpdateProductCategoriesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetProductCategories> _productcategoryRepository;

        public UpdateProductCategoriesCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetProductCategories> productcategoryRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productcategoryRepository = productcategoryRepository ?? throw new ArgumentNullException(nameof(productcategoryRepository));
        }

        public async Task<Response<bool>> Handle(UpdateProductCategoriesCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var productCategories = await _productcategoryRepository.GetByIdAsync(request.Id);
                if (productCategories == null)
                {
                    _logger.LogWarning($"Product update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                productCategories.Name = request.Name;
                productCategories.CategoryCode = request.CategoryCode;
                productCategories.UpdateDate = DateTime.Now;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
