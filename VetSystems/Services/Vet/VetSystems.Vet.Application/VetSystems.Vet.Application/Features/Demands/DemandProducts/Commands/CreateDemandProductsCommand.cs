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
    public class CreateDemandProductsCommand : IRequest<Response<bool>>
    {
        public Guid id { get; set; }
        public string Remark { get; set; } = string.Empty;
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? StockState { get; set; }
        public int? isActive { get; set; }
        public decimal? Reserved { get; set; }
        public string Barcode { get; set; } = string.Empty;
    }

    public class CreateDemandProductsCommandHandler : IRequestHandler<CreateDemandProductsCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(CreateDemandProductsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Vet.Domain.Entities.VetDemandProducts demandProducts = new()
                {
                    id = Guid.NewGuid(),
                    Remark = request.Remark,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice,
                    Amount = request.Amount,
                    StockState = request.StockState,
                    isActive = request.isActive,
                    Reserved = request.Reserved,
                    Barcode = request.Barcode,
                    CreateDate = DateTime.Now,
                };
                await _demandProductsRepository.AddAsync(demandProducts);
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
