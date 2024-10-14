using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Application.Models.Accounting;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Accounting.Commands
{
    public class UpdateSaleCommand : IRequest<Response<SaleResponseDto>>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; } = string.Empty;
        public List<SaleTransRequestDto> Trans { get; set; }
    }

    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Response<SaleResponseDto>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;
        private readonly IRepository<VetSaleBuyTrans> _saleBuyTransRepository;

        public UpdateSaleCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateSaleCommandHandler> logger, IRepository<VetSaleBuyOwner> saleBuyOwnerRepository, IRepository<VetProducts> productRepository, IRepository<VetTaxis> taxisRepository, IRepository<VetSaleBuyTrans> saleBuyTransRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _saleBuyOwnerRepository = saleBuyOwnerRepository;
            _productRepository = productRepository;
            _taxisRepository = taxisRepository;
            _saleBuyTransRepository = saleBuyTransRepository;
        }

        public async Task<Response<SaleResponseDto>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {

            var response = Response<SaleResponseDto>.Success(200);
            response.Data = new SaleResponseDto();
            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                VetSaleBuyOwner saleBuyOwner = await _saleBuyOwnerRepository.GetByIdAsync(request.Id);
                if (saleBuyOwner == null)
                {
                    return Response<SaleResponseDto>.Fail("Not Found SaleBuy", 404);
                }
                saleBuyOwner.Date = request.Date;
                saleBuyOwner.Remark = request.Remark;

                foreach (var item in request.Trans)
                {
                    Vet.Domain.Entities.VetProducts _product = await _productRepository.GetByIdAsync(item.Product);
                    var taxis = await _taxisRepository.GetByIdAsync(_product.TaxisId.GetValueOrDefault());
                    decimal vatAmaount = CalculateVatAmount(_product.SellingPrice, item.Quantity, (taxis == null ? 0 : taxis.TaxRatio), _product.SellingIncludeKDV.GetValueOrDefault());

                    var _trans = await _saleBuyTransRepository.GetByIdAsync(item.Id);
                    if (_trans == null)
                    {
                        if (_product != null)
                        {
                 
                            var newTrans = new Vet.Domain.Entities.VetSaleBuyTrans
                            {
                                Id = Guid.NewGuid(),
                                InvoiceNo = saleBuyOwner.InvoiceNo,
                                Ratio = _product.Ratio,
                                ProductId = _product.Id,
                                CreateDate = DateTime.Now,
                                CreateUsers = _identity.Account.UserName,
                                Amount = item.UnitPrice,
                                VatIncluded = _product.SellingIncludeKDV,
                                VatAmount = vatAmaount,
                                Price = _product.SellingPrice,
                                OwnerId = saleBuyOwner.Id,
                                Discount = item.Discount,
                                Quantity = item.Quantity,
                                TaxisId = taxis.Id,
                                NetPrice = (_product.SellingIncludeKDV.GetValueOrDefault() || _product.BuyingIncludeKDV.GetValueOrDefault()) ? (Math.Round((_product.SellingPrice) * item.Quantity, 2, MidpointRounding.ToEven) - vatAmaount) : Math.Round((_product.SellingPrice) * item.Quantity, 2, MidpointRounding.ToEven),
                            };
                            newTrans.IsNew = true;
                            saleBuyOwner.VetSaleBuyTrans.Add(newTrans);
                            //await _saleBuyTransRepository.AddAsync(newTrans);
                        }
                    }
                    else
                    {
                        _trans.IsNew = false;
                        _trans.ProductId = _product.Id;
                        _trans.InvoiceNo = saleBuyOwner.InvoiceNo;
                        _trans.Ratio = _product.Ratio;
                        _trans.UpdateDate = DateTime.Now;
                        _trans.UpdateUsers = _identity.Account.UserName;
                        _trans.Amount = item.UnitPrice;
                        _trans.VatIncluded = _product.SellingIncludeKDV;
                        _trans.VatAmount = vatAmaount;
                        _trans.Price = _product.SellingPrice;
                        _trans.OwnerId = saleBuyOwner.Id;
                        _trans.Discount = item.Discount;
                        _trans.Quantity = item.Quantity;
                        _trans.TaxisId = taxis.Id;
                        _trans.NetPrice = (_product.SellingIncludeKDV.GetValueOrDefault() || _product.BuyingIncludeKDV.GetValueOrDefault()) ? (Math.Round((_product.SellingPrice) * item.Quantity, 2, MidpointRounding.ToEven) - vatAmaount) : Math.Round((_product.SellingPrice) * item.Quantity, 2, MidpointRounding.ToEven);
                        //_saleBuyTransRepository.Update(_trans);

                    }   
                }
                saleBuyOwner.KDV = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven);
                saleBuyOwner.NetPrice = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven);
                saleBuyOwner.Total = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven)
                                            + Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven);
                saleBuyOwner.Discount = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.Discount.GetValueOrDefault()), 2, MidpointRounding.ToEven);

                foreach (var item in saleBuyOwner.VetSaleBuyTrans)
                {
                    if (item.IsNew)
                        await _saleBuyTransRepository.AddAsync(item);
                    else
                        _saleBuyTransRepository.Update(item);
                }

                await _uow.SaveChangesAsync(cancellationToken);

                response.Data.Id = saleBuyOwner.Id;
                response.Data.Amount = saleBuyOwner.Total.GetValueOrDefault();

            }
            catch (Exception ex)
            {
                return Response<SaleResponseDto>.Fail(ex.Message, 400);
            }
            return response;

        }

        private decimal CalculateVatAmount(decimal _amount, decimal _quentity, decimal _ratio, bool _vatInclude)
        {
            decimal vatAmount = 0;
            try
            {
                if (_ratio != 0)
                {
                    decimal basePrice = 0;
                    if (_vatInclude)
                    {
                        basePrice = (_amount * _quentity) / (1 + (Convert.ToDecimal(_ratio) / 100));
                        vatAmount = Math.Round((_amount * _quentity) - basePrice, 2, MidpointRounding.ToZero);
                    }
                    else
                    {
                        vatAmount = _ratio * (_amount * _quentity) / 100;
                    }
                }
            }
            catch (Exception)
            {
            }
            return vatAmount;
        }

    }
}
