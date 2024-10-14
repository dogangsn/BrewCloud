using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Accounting;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Accounting.Queries
{
    public class GetSalesByIdQuery : IRequest<Response<SalesOwnerByIdListDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetSalesByIdQueryHandler : IRequestHandler<GetSalesByIdQuery, Response<SalesOwnerByIdListDto>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<VetSaleBuyTrans> _saleBuyTransRepository;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public GetSalesByIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetSaleBuyOwner> saleBuyOwnerRepository, IRepository<VetSaleBuyTrans> saleBuyTransRepository, IRepository<VetTaxis> taxisRepository, IRepository<VetProducts> productRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _saleBuyOwnerRepository = saleBuyOwnerRepository;
            _saleBuyTransRepository = saleBuyTransRepository;
            _taxisRepository = taxisRepository;
            _productRepository = productRepository;
        }

        public async Task<Response<SalesOwnerByIdListDto>> Handle(GetSalesByIdQuery request, CancellationToken cancellationToken)
        {
            var response = Response<SalesOwnerByIdListDto>.Success(200);
            try
            {
                VetSaleBuyOwner buyOwner = await _saleBuyOwnerRepository.GetByIdAsync(request.Id);
                if (buyOwner != null)
                {
                    response.Data = new SalesOwnerByIdListDto()
                    {
                        Id = buyOwner.Id,
                        Date = buyOwner.Date,
                        Remark = buyOwner.Remark,
                    };
                    response.Data.Trans = new List<SaleTransRequestDto>();
                    List<VetSaleBuyTrans> _buytrans = (await _saleBuyTransRepository.GetAsync(x=> x.OwnerId == buyOwner.Id)).ToList();
                    if (_buytrans.Count > 0)
                    {
                        foreach(var x in _buytrans)
                        {
                            Vet.Domain.Entities.VetProducts _product =  await _productRepository.GetByIdAsync(x.ProductId.GetValueOrDefault());
                            var taxis = await  _taxisRepository.GetByIdAsync(_product.TaxisId.GetValueOrDefault());

                            SaleTransRequestDto saleTrans = new SaleTransRequestDto()
                            {
                                Id = x.Id,
                                Discount = x.Discount.GetValueOrDefault(),
                                Product = x.ProductId.GetValueOrDefault(),
                                Quantity = x.Quantity,
                                Vat = taxis.Id.ToString(),
                                Unit = _product.UnitId.GetValueOrDefault().ToString(),
                                UnitPrice = _product.SellingPrice                                
                            };
                            response.Data.Trans.Add(saleTrans);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Response<SalesOwnerByIdListDto>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
