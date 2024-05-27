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
namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands
{
    public class CreateProductDescriptionCommand : IRequest<Response<bool>>
    {
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
        public int? AnimalType { get; set; }
        public int? NumberRepetitions { get; set; }
        public Guid StoreId { get; set; }
        public Guid TaxisId { get; set; }


    }

    public class CreateProductDescriptionCommandHandler : IRequestHandler<CreateProductDescriptionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public CreateProductDescriptionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<VetProducts> productRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Response<bool>> Handle(CreateProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                VetProducts _product = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    UnitId = request.UnitId,
                    CategoryId = request.CategoryId,
                    ProductTypeId = request.ProductTypeId.GetValueOrDefault(),
                    SupplierId = request.SupplierId,
                    ProductCode = request.ProductCode,
                    ProductBarcode = request.ProductBarcode,
                    Ratio = request.Ratio,
                    Active = false,
                    BuyingPrice = request.BuyingPrice,
                    CriticalAmount = request.CriticalAmount,
                    FixPrice = request.FixPrice,
                    IsExpirationDate = request.IsExpirationDate,
                    SellingIncludeKDV = request.SellingIncludeKDV,
                    SellingPrice = request.SellingPrice,
                    BuyingIncludeKDV= request.BuyingIncludeKDV,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    AnimalType = request.AnimalType,
                    NumberRepetitions = request.NumberRepetitions,
                    StoreId = request.StoreId,
                    TaxisId = request.TaxisId,
                };
                await _productRepository.AddAsync(_product);
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
