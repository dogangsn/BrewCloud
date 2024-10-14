using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Shared.Service;
using AutoMapper;


namespace BrewCloud.Vet.Application.Features.Definition.StockTracking.Commands
{
    public class CreateStockTrackingCommand : IRequest<Response<bool>>
    {
        public Guid ProductId { get; set; }
        public bool Status { get; set; } = true;
        public ProcessTypes ProcessType { get; set; } = ProcessTypes.NewStock;
        public StockTrackingType Type { get; set; }
        public decimal Piece { get; set; }
        public Guid? SupplierId { get; set; }  
        public DateTime? ExpirationDate { get; set; }
        public decimal PurchasePrice { get; set; }
    }

    public class CreateStockTrackingCommandHandler : IRequestHandler<CreateStockTrackingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStockTrackingCommandHandler> _logger;
        private readonly IRepository<VetStockTracking> _vetStockTrackingrepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;


        public CreateStockTrackingCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateStockTrackingCommandHandler> logger, IRepository<VetStockTracking> vetStockTrackingrepository, IRepository<VetProducts> productRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetStockTrackingrepository = vetStockTrackingrepository;
            _productRepository = productRepository;
        }

        public async Task<Response<bool>> Handle(CreateStockTrackingCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {

                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    return Response<bool>.Fail("Property update failed", 404);
                }


                VetStockTracking stockTracking = new()
                {
                    Id = Guid.NewGuid(),
                    Piece = request.Piece,
                    ProductId = request.ProductId,
                    ProcessType = request.ProcessType,
                    Type = request.Type,
                    SupplierId = request.SupplierId,
                    ExpirationDate = request.ExpirationDate,
                    PurchasePrice = request.PurchasePrice,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };

                await _vetStockTrackingrepository.AddAsync(stockTracking);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

            }
            return response;

        }
    }
}
