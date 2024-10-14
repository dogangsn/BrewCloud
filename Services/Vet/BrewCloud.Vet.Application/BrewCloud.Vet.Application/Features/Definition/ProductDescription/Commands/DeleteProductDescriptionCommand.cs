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
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.ProductDescription.Commands
{
    public class DeleteProductDescriptionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductDescriptionCommandHandler : IRequestHandler<DeleteProductDescriptionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductDescriptionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyTrans> _saleBuyTransRepository;
        private readonly IRepository<Vet.Domain.Entities.VetDemandTrans> _demandTransRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteProductDescriptionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, 
                                                      ILogger<DeleteProductDescriptionCommandHandler> logger, 
                                                      IRepository<Domain.Entities.VetProducts> productRepository, 
                                                      IIdentityRepository identityRepository,
                                                      IRepository<Domain.Entities.VetSaleBuyTrans> saleBuyTransRepository,
                                                      IRepository<Domain.Entities.VetDemandTrans> demandTransRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _saleBuyTransRepository = saleBuyTransRepository ?? throw new ArgumentNullException(nameof(saleBuyTransRepository));
            _demandTransRepository = demandTransRepository ?? throw new ArgumentNullException(nameof(demandTransRepository));
        }
        public async Task<Response<bool>> Handle(DeleteProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                if (product == null)
                {
                    _logger.LogWarning($"Product update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                var IsBuySaleRecord = await _saleBuyTransRepository.FirstOrDefaultAsync(x=>x.ProductId == request.Id && x.Deleted == false);
                if (IsBuySaleRecord != null)
                {
                    _logger.LogWarning($"Hareket Görmüş Kayıt : {IsBuySaleRecord.Id}");
                    return Response<bool>.Fail("Hareket Görmüş Kayıt Silinemez", 404);
                }
                var IsDemandRecord = await _demandTransRepository.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.Deleted == false);
                if (IsBuySaleRecord != null)
                {
                    _logger.LogWarning($"Sipariş Hareketi Görmüş Kayıt : {IsDemandRecord.Id}");
                    return Response<bool>.Fail("Sipariş Hareketi Mevcut Silinemez", 404);
                }

                product.Deleted = true;
                product.DeletedDate = DateTime.Now;
                product.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
