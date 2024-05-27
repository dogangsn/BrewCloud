using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Definition.StockTracking.Commands
{
    public class DeleteStockTrackingCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStockTrackingCommandHandler : IRequestHandler<DeleteStockTrackingCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStockTrackingCommandHandler> _logger;
        private readonly IRepository<VetStockTracking> _vetStockTrackingrepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public DeleteStockTrackingCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteStockTrackingCommandHandler> logger, IRepository<VetStockTracking> vetStockTrackingrepository, IRepository<VetProducts> productRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetStockTrackingrepository = vetStockTrackingrepository;
            _productRepository = productRepository;
        }

        public async Task<Response<bool>> Handle(DeleteStockTrackingCommand request, CancellationToken cancellationToken)
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

                stocktracking.Deleted = true;
                stocktracking.DeletedDate = DateTime.Now;
                stocktracking.DeletedUsers = _identity.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                response.Data = false;
            }
            return response;

        }
    }
}
