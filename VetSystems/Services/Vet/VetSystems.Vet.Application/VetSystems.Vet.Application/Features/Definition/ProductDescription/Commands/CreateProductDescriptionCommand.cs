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
        public Guid? ProductTypeId { get; set; }
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

    }

    public class CreateProductDescriptionCommandHandler : IRequestHandler<CreateProductDescriptionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.Products> _customerRepository;

        public CreateProductDescriptionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Products> customerRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public Task<Response<bool>> Handle(CreateProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
