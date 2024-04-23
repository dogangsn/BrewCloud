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

namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands
{
    public class UpdateProductDescriptionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UnitId { get; set; }
        public Guid? CategoryId { get; set; }
        public int? ProductTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public string ProductBarcode { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public decimal Ratio { get; set; } = 0;
        public decimal BuyingPrice { get; set; } = 0;
        public decimal SellingPrice { get; set; } = 0;
        public decimal CriticalAmount { get; set; } = 0;
        public bool? Active { get; set; } = true;
        public bool? SellingIncludeKDV { get; set; } = false;
        public bool? BuyingIncludeKDV { get; set; } = false;
        public bool? FixPrice { get; set; } = false;
        public bool? IsExpirationDate { get; set; } = false;
        public int? NumberRepetitions { get; set; } 
        public int? AnimalType { get; set; }
        public Guid StoreId { get; set; }
        public Guid TaxisId { get; set; }
    }

    public class UpdateProductDescriptionCommandHandler : IRequestHandler<UpdateProductDescriptionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductDescriptionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IIdentityRepository _identityRepository;

        public UpdateProductDescriptionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateProductDescriptionCommandHandler> logger, IRepository<Domain.Entities.VetProducts> productRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }
        public async Task<Response<bool>> Handle(UpdateProductDescriptionCommand request, CancellationToken cancellationToken)
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
                product.BuyingIncludeKDV = request.BuyingIncludeKDV;
                product.SellingIncludeKDV = request.SellingIncludeKDV;
                product.BuyingPrice = request.BuyingPrice;
                product.SellingPrice = request.SellingPrice;
                product.FixPrice = request.FixPrice;    
                product.IsExpirationDate = request.IsExpirationDate;
                product.CategoryId = request.CategoryId;
                product.CriticalAmount = request.CriticalAmount;
                product.Name = request.Name;
                product.ProductBarcode = request.ProductBarcode;
                request.SupplierId = request.SupplierId;
                product.ProductTypeId = request.ProductTypeId.GetValueOrDefault();
                product.UnitId = request.UnitId;
                product.Ratio = request.Ratio;
                product.UpdateDate = DateTime.Now;
                product.UpdateUsers = _identityRepository.Account.UserName;
                product.ProductCode = request.ProductCode;
                product.NumberRepetitions = request.NumberRepetitions.GetValueOrDefault();
                product.AnimalType = request.AnimalType.GetValueOrDefault();
                product.StoreId = request.StoreId;
                product.TaxisId = request.TaxisId;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
