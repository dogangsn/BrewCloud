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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Demands.DemandProducts.Commands
{
    public class CreateDemandProductsCommand : IRequest<Response<VetDemandProducts>>
    {
        public Guid id { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? StockState { get; set; }
        public int? isActive { get; set; }
        public decimal? Reserved { get; set; }
        public string Barcode { get; set; } = string.Empty;
    }

    public class CreateDemandProductsCommandHandler : IRequestHandler<CreateDemandProductsCommand, Response<VetDemandProducts>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDemandProductsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetDemandProducts> _demandProductsRepository;

        public CreateDemandProductsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateDemandProductsCommandHandler> logger, IRepository<Domain.Entities.VetDemandProducts> demandProductsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandProductsRepository = demandProductsRepository ?? throw new ArgumentNullException(nameof(demandProductsRepository));
        }

        public async Task<Response<VetDemandProducts>> Handle(CreateDemandProductsCommand request, CancellationToken cancellationToken)
         {
            var response = new Response<VetDemandProducts>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Vet.Domain.Entities.VetDemandProducts demandProducts = new()
                {
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice,
                    Amount = request.Amount,
                    StockState = request.StockState,
                    isActive = request.isActive,
                    Reserved = request.Reserved,
                    Barcode = request.Barcode,
                    CreateDate = DateTime.Now,
                    TaxisId = Guid.Empty
                };
                await _demandProductsRepository.AddAsync(demandProducts);
                await _uow.SaveChangesAsync(cancellationToken);
                response.Data = demandProducts;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
           
            return response;

        }
    }
}
