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

namespace VetSystems.Vet.Application.Features.Demands.DemandProducts.Commands
{

    public class UpdateDemandProductsCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Remark { get; set; } = string.Empty;
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? StockState { get; set; }
        public int? isActive { get; set; }
        public decimal? Reserved { get; set; }
        public string Barcode { get; set; } = string.Empty;
    }

    public class UpdateDemandProductsCommandHandler : IRequestHandler<UpdateDemandProductsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDemandProductsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetDemandProducts> _demandProductsRepository;

        public UpdateDemandProductsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateDemandProductsCommandHandler> logger, IRepository<Domain.Entities.VetDemandProducts> demandProductsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandProductsRepository = demandProductsRepository ?? throw new ArgumentNullException(nameof(demandProductsRepository));
        }

        public async Task<Response<bool>> Handle(UpdateDemandProductsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var demandProducts = await _demandProductsRepository.GetByIdAsync(request.Id);
                if (demandProducts == null)
                {
                    _logger.LogWarning($"Casing update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                demandProducts.Remark = request.Remark;
                demandProducts.Quantity = request.Quantity;
                demandProducts.UnitPrice = request.UnitPrice;
                demandProducts.Amount = request.Amount;
                demandProducts.StockState = request.StockState;
                demandProducts.isActive = request.isActive;
                demandProducts.Reserved = request.Reserved;
                demandProducts.Barcode = request.Barcode;
                demandProducts.UpdateDate = DateTime.Now;
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
