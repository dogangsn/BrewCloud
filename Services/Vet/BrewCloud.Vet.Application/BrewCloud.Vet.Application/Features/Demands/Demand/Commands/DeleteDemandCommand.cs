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
using BrewCloud.Vet.Application.Models.Demands.Demands;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Demands.Demand.Commands
{

    public class DeleteDemandCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteDemandCommandHandler : IRequestHandler<DeleteDemandCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDemandCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetDemands> _demandsRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<Vet.Domain.Entities.VetDemandProducts> _demandProductsRepository;
        private readonly IRepository<Vet.Domain.Entities.VetDemandTrans> _demandTransRepository;

        public DeleteDemandCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteDemandCommandHandler> logger, IRepository<Domain.Entities.VetDemands> demandsRepository, IIdentityRepository identityRepository, IRepository<Domain.Entities.VetDemandProducts> demandProductsRepository, IRepository<Domain.Entities.VetDemandTrans> demandTransRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demandsRepository = demandsRepository ?? throw new ArgumentNullException(nameof(demandsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _demandProductsRepository = demandProductsRepository ?? throw new ArgumentNullException(nameof(demandProductsRepository));
            _demandTransRepository = demandTransRepository ?? throw new ArgumentNullException(nameof(demandTransRepository));
        }

        public async Task<Response<bool>> Handle(DeleteDemandCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var demands = await _demandsRepository.GetByIdAsync(request.Id);
                if (demands == null)
                {
                    _logger.LogWarning($"Demand update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                demands.Deleted = true;
                demands.DeletedDate = DateTime.Now;
                demands.DeletedUsers = _identityRepository.Account.UserName;
                await _uow.SaveChangesAsync(cancellationToken);
                 


                 string query = "Select * from vetdemandproducts where Deleted=1 and ownerid = @ownerid";
                var products = _uow.Query<VetDemandProducts>(query, new { ownerid = demands.Id }).ToList();
                if (products == null)
                {
                    _logger.LogWarning($"DemandProducts update failed. Id number: {demands.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                foreach (var item in products)
                {
                    var product = await _demandProductsRepository.GetByIdAsync(item.Id);
                    product.Deleted = false;
                    product.DeletedDate = DateTime.Now;
                    product.DeletedUsers = _identityRepository.Account.UserName;
                    product.UpdateDate = DateTime.Now;
                    product.UpdateUsers = _identityRepository.Account.UserName;
                    await _uow.SaveChangesAsync(cancellationToken);
                }
                string querys = "Select * from vetdemandTrans where Deleted=0 and ownerid = @ownerid";
                var transList = _uow.Query<VetDemandProducts>(querys, new { ownerid = demands.Id }).ToList();
                if (transList == null)
                {
                    _logger.LogWarning($"DemandTrans update failed. Id number: {demands.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                foreach (var item in transList)
                {
                    var trans = await _demandTransRepository.GetByIdAsync(item.Id);

                    trans.Deleted = true;
                    trans.DeletedDate = DateTime.Now;
                    trans.DeletedUsers = _identityRepository.Account.UserName;
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
