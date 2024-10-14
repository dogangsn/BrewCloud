using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Demands.DemandProducts;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Demands.Demand.Commands
{
    public class CreateDemandCommand : IRequest<Response<VetDemands>>
    {
        public Guid? id { get; set; }
        public DateTime? date { get; set; }
        public string documentno { get; set; }
        public Guid? suppliers { get; set; }
        public DateTime? deliverydate { get; set; }
        public string note { get; set; }
        public int? state { get; set; }
        public bool? iscomplated { get; set; }
        public List<DemandProductsDto> demandProductList { get; set; }
    }

    public class CreateDemandCommandHandler : IRequestHandler<CreateDemandCommand, Response<VetDemands>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDemandCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetDemands> _demandsRepository;
        private readonly IRepository<Vet.Domain.Entities.VetDemandProducts> _demandProductsRepository;
        private readonly IRepository<Vet.Domain.Entities.VetDemandTrans> _demandTransRepository;
        private readonly IIdentityRepository _identityRepository;


        public CreateDemandCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateDemandCommandHandler> logger, IRepository<Domain.Entities.VetDemands> demandsRepository, IRepository<Domain.Entities.VetDemandProducts> demandProductsRepository, IRepository<Domain.Entities.VetDemandTrans> demandTransRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandsRepository = demandsRepository ?? throw new ArgumentNullException(nameof(demandsRepository));
            _identityRepository = identity ?? throw new ArgumentNullException(nameof(identity));
            _demandProductsRepository = demandProductsRepository ?? throw new ArgumentNullException(nameof(demandProductsRepository));
            _demandTransRepository = demandTransRepository ?? throw new ArgumentNullException(nameof(demandTransRepository));

        }

        public async Task<Response<VetDemands>> Handle(CreateDemandCommand request, CancellationToken cancellationToken)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.Local;
            var response = new Response<VetDemands>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {
                Vet.Domain.Entities.VetDemands demands = new()
                {
                    Id = Guid.NewGuid(),
                    suppliers = request.suppliers,
                    date = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.date), timeZone) ,
                    documentno = request.documentno,
                    deliverydate = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.deliverydate),timeZone),
                    note = request.note,
                    state = request.state,
                    iscomplated = false,
                    isBuying = false,
                    isAccounting = false,
                    CreateDate = DateTime.Now,
                };
                await _demandsRepository.AddAsync(demands);
                await _uow.SaveChangesAsync(cancellationToken);
                  response.Data = demands;

                foreach (var item in request.demandProductList)
                {
                    var demandProducts = await _demandProductsRepository.GetByIdAsync(item.id);
                    if (demandProducts == null)
                    {
                        _logger.LogWarning($"demandProduct update failed. Id number: {request.id}");
                        return Response<VetDemands>.Fail("Property update failed", 404);
                    }

                    demandProducts.OwnerId = demands.Id;
                    demandProducts.Deleted = true;
                    demandProducts.DeletedDate = DateTime.Now;
                    demandProducts.DeletedUsers = _identityRepository.Account.UserName;
                    await _uow.SaveChangesAsync(cancellationToken);

                    Vet.Domain.Entities.VetDemandTrans demandTrans = new()
                    {
                        Id = Guid.NewGuid(),
                        OwnerId = demands.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Amount = item.Amount,
                        StockState = item.StockState,
                        isActive = item.isActive,
                        Reserved = item.Reserved,
                        Barcode = item.Barcode,
                        TaxisId = item.TaxisId,
                        CreateDate = DateTime.Now,
                    };
                    await _demandTransRepository.AddAsync(demandTrans);
                    await _uow.SaveChangesAsync(cancellationToken);



                }
                



            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }

            return response;

        }
    }
}
