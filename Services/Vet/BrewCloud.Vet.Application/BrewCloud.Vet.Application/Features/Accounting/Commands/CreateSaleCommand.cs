using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Enums;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Accounting;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Accounting.Commands
{
    public class CreateSaleCommand : IRequest<Response<SaleResponseDto>>
    {
        public DateTime Date { get; set; }
        public string Remark { get; set; } = string.Empty;
        public Guid? CustomerId { get; set; }
        public List<SaleTransRequestDto>? Trans { get; set; }
        public bool IsPrice { get; set; } = false;
        public decimal Price { get; set; } = 0;
        public Guid ExaminationId { get; set; }
        public bool? IsExaminations { get; set; } = false;
        public bool? IsAccomodation { get; set; } = false;
        public Guid? AccomodationId { get; set; }

    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Response<SaleResponseDto>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;

        public CreateSaleCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSaleCommandHandler> logger, IRepository<VetSaleBuyOwner> saleBuyOwnerRepository, IRepository<VetProducts> productRepository, IRepository<VetTaxis> taxisRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _saleBuyOwnerRepository = saleBuyOwnerRepository;
            _productRepository = productRepository;
            _taxisRepository = taxisRepository;
        }

        public async Task<Response<SaleResponseDto>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var response = Response<SaleResponseDto>.Success(200);
            response.Data = new SaleResponseDto();

            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            try
            {
                Vet.Domain.Entities.VetSaleBuyOwner saleBuyOwner = new()
                {
                    Id = Guid.NewGuid(),
                    Date = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.Date), localTimeZone),
                    CustomerId = request.CustomerId,
                    SupplierId = Guid.Empty,
                    InvoiceNo = string.Empty,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Type = (int)BuySaleType.Selling,
                    Remark = request.Remark
                };

                foreach (var item in request.Trans)
                {
                    Vet.Domain.Entities.VetProducts _product = await _productRepository.GetByIdAsync(item.Product);
                    if (_product != null)
                    {
                        var taxis = await _taxisRepository.GetByIdAsync(_product.TaxisId.GetValueOrDefault());
                        decimal vatAmaount = CalculateVatAmount(_product.SellingPrice, item.Quantity, (taxis == null ? 0 : taxis.TaxRatio), _product.SellingIncludeKDV.GetValueOrDefault());

                        var _trans = new Vet.Domain.Entities.VetSaleBuyTrans
                        {
                            Id = Guid.NewGuid(),
                            InvoiceNo = saleBuyOwner.InvoiceNo,
                            Ratio = _product.Ratio,
                            ProductId = _product.Id,
                            CreateDate = DateTime.Now,
                            CreateUsers = _identity.Account.UserName,
                            Amount = item.Quantity,
                            VatIncluded = _product.SellingIncludeKDV,
                            VatAmount = vatAmaount,
                            Price = _product.SellingPrice,
                            OwnerId = saleBuyOwner.Id,
                            Discount = item.Discount,
                            Quantity = item.Quantity,
                            TaxisId = taxis.Id,
                            NetPrice = (_product.SellingIncludeKDV.GetValueOrDefault() || _product.BuyingIncludeKDV.GetValueOrDefault()) ? (Math.Round((_product.SellingPrice) * item.Quantity, 2, MidpointRounding.ToEven) - vatAmaount) : Math.Round((_product.SellingPrice) * item.Quantity, 2, MidpointRounding.ToEven),
                        };
                        saleBuyOwner.VetSaleBuyTrans.Add(_trans);
                    }
                }


                if ((request.IsExaminations.GetValueOrDefault() || request.IsAccomodation.GetValueOrDefault()) && request.Price > 0)
                {
                    var _trans = new Vet.Domain.Entities.VetSaleBuyTrans
                    {
                        Id = Guid.NewGuid(),
                        InvoiceNo = saleBuyOwner.InvoiceNo,
                        Ratio = 0,
                        ProductId = Guid.Empty,
                        CreateDate = DateTime.Now,
                        CreateUsers = _identity.Account.UserName,
                        Amount = 1,
                        VatIncluded = false,
                        VatAmount = 0,
                        Price = request.Price,
                        OwnerId = saleBuyOwner.Id,
                        Discount = 0,
                        Quantity = 1,
                        TaxisId = Guid.Empty,
                        NetPrice = Math.Round((request.Price) * 1, 2, MidpointRounding.ToEven),
                    };
                    saleBuyOwner.VetSaleBuyTrans.Add(_trans);
                }

                saleBuyOwner.KDV = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven);
                saleBuyOwner.NetPrice = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven);
                saleBuyOwner.Total = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven)
                                       + Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven)
                                       - Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.Discount.GetValueOrDefault()), 2, MidpointRounding.ToEven);

                saleBuyOwner.Discount = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.Discount.GetValueOrDefault()), 2, MidpointRounding.ToEven);

                if (request.IsExaminations.GetValueOrDefault())
                {
                    saleBuyOwner.IsExaminations = true;
                    saleBuyOwner.ExaminationsId = request.ExaminationId;
                }

                if (request.IsAccomodation.GetValueOrDefault())
                {
                    saleBuyOwner.IsAccomodation = true;
                    saleBuyOwner.AccomodationId = request.AccomodationId;
                }
                 
                await _saleBuyOwnerRepository.AddAsync(saleBuyOwner);
                await _uow.SaveChangesAsync(cancellationToken);

                saleBuyOwner.InvoiceNo = ("#" + Convert.ToString(saleBuyOwner.RecordId));
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
