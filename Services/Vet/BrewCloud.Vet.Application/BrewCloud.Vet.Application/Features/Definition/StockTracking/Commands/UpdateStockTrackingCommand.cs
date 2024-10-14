 
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Shared.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace BrewCloud.Vet.Application.Features.Definition.StockTracking.Commands
{
    public class UpdateStockTrackingCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; } 
        public bool Status { get; set; } = true;
        public ProcessTypes ProcessType { get; set; } = ProcessTypes.NewStock;
        public StockTrackingType Type { get; set; }
        public decimal Piece { get; set; }
        public Guid? SupplierId { get; set; }  
        public DateTime? ExpirationDate { get; set; }
        public decimal PurchasePrice { get; set; }
    }

    public class UpdateStockTrackingCommandHandler : IRequestHandler<UpdateStockTrackingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStockTrackingCommandHandler> _logger;
        private readonly IRepository<VetStockTracking> _vetStockTrackingrepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public UpdateStockTrackingCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateStockTrackingCommandHandler> logger, IRepository<VetStockTracking> vetStockTrackingrepository, IRepository<VetProducts> productRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetStockTrackingrepository = vetStockTrackingrepository;
            _productRepository = productRepository;
        }

        public async Task<Response<bool>> Handle(UpdateStockTrackingCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

            try
            {
                var stocktracking = await _vetStockTrackingrepository.GetByIdAsync(request.Id);
                if (stocktracking == null)
                {
                    _logger.LogWarning($"stocktracking update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("stocktracking update failed", 404);
                }

                stocktracking.UpdateDate = DateTime.Now;
                stocktracking.UpdateUsers = _identity.Account.UserName;
                stocktracking.Status = request.Status;
                stocktracking.ProcessType = request.ProcessType;
                stocktracking.Type = request.Type;
                stocktracking.Piece = request.Piece;
                stocktracking.SupplierId = request.SupplierId;
                stocktracking.ExpirationDate = request.ExpirationDate;
                stocktracking.PurchasePrice = request.PurchasePrice;

                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
            }
            return response;
             
        }
    }
}
