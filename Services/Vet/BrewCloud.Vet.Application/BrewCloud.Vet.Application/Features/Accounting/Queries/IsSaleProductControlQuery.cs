using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Accounting.Queries
{
    public class IsSaleProductControlQuery : IRequest<Response<string>>
    {
        public List<Guid>? ProductIds { get; set; } 
    }

    public class IsSaleProductControlQueryHandler : IRequestHandler<IsSaleProductControlQuery, Response<string>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IRepository<VetStockTracking> _vetStockTrackingrepository;

        public IsSaleProductControlQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetProducts> productRepository, IRepository<VetStockTracking> vetStockTrackingrepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _productRepository = productRepository;
            _vetStockTrackingrepository = vetStockTrackingrepository;
        }

        public async Task<Response<string>> Handle(IsSaleProductControlQuery request, CancellationToken cancellationToken)
        {

            var response = Response<string>.Success(200);
            try
            {
                string _responseData = string.Empty;

                foreach (var item in request.ProductIds)
                {
                    var product = await _productRepository.GetByIdAsync(item);
                    if (product != null)
                    {
                        var _tracking = (await _vetStockTrackingrepository.GetAsync(x => x.ProductId == product.Id && x.Deleted == false)).ToList();
                        if (!_tracking.Any())
                        {
                            _responseData += product.Name + " Stoklarda Mevcut Değildir." + Environment.NewLine;
                        }
                    }
                }
                _responseData += "Kayıt etmek istiyor musunuz?";

                response.Data = _responseData;
            }
            catch (Exception ex)
            {
                return Response<string>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
