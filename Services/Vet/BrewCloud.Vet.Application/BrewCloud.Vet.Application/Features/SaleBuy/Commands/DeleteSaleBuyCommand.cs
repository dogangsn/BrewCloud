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
using BrewCloud.Vet.Application.Features.Definition.UnitDefinitions.Commands;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.SaleBuy.Commands
{
    public class DeleteSaleBuyCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public bool? AllSaleDeleted { get; set; } = false;
    }

    public class DeleteSaleBuyCommandHandler : IRequestHandler<DeleteSaleBuyCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteSaleBuyCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyTrans> _saleBuyTransRepository;
        private readonly IRepository<Vet.Domain.Entities.VetDemands> _vetDemandsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteSaleBuyCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteSaleBuyCommandHandler> logger, IRepository<Domain.Entities.VetSaleBuyOwner> saleBuyOwnerRepository, IRepository<Domain.Entities.VetSaleBuyTrans> saleBuyTransRepository, IIdentityRepository identityRepository, IRepository<Domain.Entities.VetDemands> vetDemandsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _saleBuyOwnerRepository = saleBuyOwnerRepository ?? throw new ArgumentNullException(nameof(saleBuyOwnerRepository));
            _saleBuyTransRepository = saleBuyTransRepository ?? throw new ArgumentNullException(nameof(saleBuyTransRepository));
            _vetDemandsRepository = vetDemandsRepository ?? throw new ArgumentNullException(nameof(vetDemandsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteSaleBuyCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                if (request.AllSaleDeleted.GetValueOrDefault(false))
                {
                    var salebuyOwner = await _saleBuyOwnerRepository.GetByIdAsync(request.OwnerId);
                    if (salebuyOwner == null)
                    {
                        _logger.LogWarning($"Untis update failed. Id number: {request.OwnerId}");
                        return Response<bool>.Fail("Property update failed", 404);
                    }
                    salebuyOwner.Deleted = true;
                    salebuyOwner.DeletedDate = DateTime.Now;
                    salebuyOwner.DeletedUsers = _identityRepository.Account.UserName;
                    if (salebuyOwner.demandsGuidId != null && salebuyOwner.demandsGuidId != Guid.Empty)
                    {
                        Guid demandsId = (Guid)salebuyOwner.demandsGuidId;
                        var demands = await _vetDemandsRepository.GetByIdAsync(demandsId);
                        if (demands == null)
                        {
                            _logger.LogWarning($"demands update failed. Id number: {salebuyOwner.demandsGuidId}");
                            return Response<bool>.Fail("Property update failed", 404);
                        }
                        demands.isBuying = false;
                    }
                    List<Vet.Domain.Entities.VetSaleBuyTrans> trans = (await _saleBuyTransRepository.GetAsync(x => x.OwnerId == request.Id)).ToList();
                    if (trans != null)
                    {
                        foreach (var item in trans)
                        {
                            item.Deleted = true;
                            salebuyOwner.DeletedDate = DateTime.Now;
                            salebuyOwner.DeletedUsers = _identityRepository.Account.UserName;
                        }
                    }
                }
                else
                {
                    var salebuytrans = await _saleBuyTransRepository.GetByIdAsync(request.Id);
                    if (salebuytrans == null)
                    {
                        _logger.LogWarning($"Untis update failed. Id number: {request.Id}");
                        return Response<bool>.Fail("Property update failed", 404);
                    }
                    salebuytrans.Deleted = true;
                    salebuytrans.DeletedDate = DateTime.Now;
                    salebuytrans.DeletedUsers = _identityRepository.Account.UserName;

                    List<Vet.Domain.Entities.VetSaleBuyTrans> trans = (await _saleBuyTransRepository.GetAsync(x => x.OwnerId == request.OwnerId && x.Deleted == false)).ToList();
                    if (trans.Count == 1)
                    {
                        var salebuyOwner = await _saleBuyOwnerRepository.GetByIdAsync(request.OwnerId);
                        if (salebuyOwner == null)
                        {
                            _logger.LogWarning($"Untis update failed. Id number: {request.OwnerId}");
                            return Response<bool>.Fail("Property update failed", 404);
                        }
                        salebuyOwner.Deleted = true;
                        salebuyOwner.DeletedDate = DateTime.Now;
                        salebuyOwner.DeletedUsers = _identityRepository.Account.UserName;
                    }
                }

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }
            return response;
        }
    }



}
