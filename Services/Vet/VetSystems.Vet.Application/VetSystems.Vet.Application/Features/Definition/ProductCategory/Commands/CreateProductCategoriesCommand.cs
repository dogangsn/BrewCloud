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

namespace VetSystems.Vet.Application.Features.Definition.ProductCategory.Commands
{
    public class CreateProductCategoriesCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; } = string.Empty;

        public string CategoryCode { get; set; } = string.Empty;
    }

    public class CreateProductCategoriesCommandHandler : IRequestHandler<CreateProductCategoriesCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetProductCategories> _productcategoryRepository;

        public CreateProductCategoriesCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetProductCategories> productcategoryRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productcategoryRepository = productcategoryRepository ?? throw new ArgumentNullException(nameof(productcategoryRepository));
        }

        public async Task<Response<bool>> Handle(CreateProductCategoriesCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetProductCategories productCategory = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    CategoryCode = request.CategoryCode,
                    CreateDate = DateTime.Now,
                };
                await _productcategoryRepository.AddAsync(productCategory);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
