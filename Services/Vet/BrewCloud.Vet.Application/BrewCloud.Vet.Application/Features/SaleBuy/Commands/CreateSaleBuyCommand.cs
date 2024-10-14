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
using BrewCloud.Vet.Application.Features.Customers.Commands;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.SaleBuy.Commands
{
    public class CreateSaleBuyCommand : IRequest<Response<bool>>
    {
        public int Type { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? SupplierId { get; set; }
        public string Remark { get; set; }
        public string InvoiceNo { get; set; }
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }
        public Guid? demandsGuidId { get; set; }

    }

    public class CreateSaleBuyCommandHandler : IRequestHandler<CreateSaleBuyCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;

        public CreateSaleBuyCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetSaleBuyOwner> salebuyownerRepository, IRepository<Vet.Domain.Entities.VetProducts> productRepository, IRepository<Domain.Entities.VetTaxis> taxisRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _saleBuyOwnerRepository = salebuyownerRepository ?? throw new ArgumentNullException(nameof(salebuyownerRepository));
            _productRepository = productRepository;
            _taxisRepository = taxisRepository;
        }
        public async Task<Response<bool>> Handle(CreateSaleBuyCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            Vet.Domain.Entities.VetProducts _product = await _productRepository.GetByIdAsync(request.ProductId.GetValueOrDefault());
            if (_product == null)
            {
                response.Data = false;
                response.IsSuccessful = false;
                return response;
            }

            Vet.Domain.Entities.VetSaleBuyOwner saleBuyOwner = new()
            {
                Id = Guid.NewGuid(),
                Date = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.Date), localTimeZone),
                CustomerId = request.CustomerId,
                Type = request.Type,
                SupplierId = request.SupplierId,
                InvoiceNo = string.IsNullOrEmpty(request.InvoiceNo) ? "#" : request.InvoiceNo,
                Total = request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice,
                CreateDate = DateTime.Now,
                CreateUsers = _identity.Account.UserName,
                PaymentType = request.PaymentType,
                demandsGuidId = request.demandsGuidId,
            };

            var taxis = await _taxisRepository.GetByIdAsync(_product.TaxisId.GetValueOrDefault());
            decimal vatAmaount = CalculateVatAmount((request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice), request.Amount, taxis.TaxRatio, (request.Type == (int)BuySaleType.Selling ? _product.SellingIncludeKDV.GetValueOrDefault() : _product.BuyingIncludeKDV.GetValueOrDefault()));

            saleBuyOwner.addSaleBuyTrans(new Vet.Domain.Entities.VetSaleBuyTrans
            {
                Id = Guid.NewGuid(),
                InvoiceNo = saleBuyOwner.InvoiceNo,
                Ratio = taxis.TaxRatio,
                ProductId = request.ProductId.GetValueOrDefault(),
                CreateDate = DateTime.Now,
                CreateUsers = _identity.Account.UserName,
                Amount = request.Amount,
                Quantity = Convert.ToInt32(request.Amount),
                VatIncluded = request.Type == (int)BuySaleType.Selling ? _product.SellingIncludeKDV : _product.BuyingIncludeKDV,
                OwnerId = saleBuyOwner.Id,
                VatAmount = vatAmaount,
                Price = request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice,
                NetPrice = (_product.SellingIncludeKDV.GetValueOrDefault() || _product.BuyingIncludeKDV.GetValueOrDefault())  
                                    ? (Math.Round((request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice) * request.Amount, 2, MidpointRounding.ToEven) - vatAmaount) 
                                    : (Math.Round((request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice) * request.Amount, 2, MidpointRounding.ToEven) + vatAmaount),
                                
            });

            saleBuyOwner.KDV = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven);

            saleBuyOwner.NetPrice = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven);

            saleBuyOwner.Total = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven); //+ Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven); 
              
            saleBuyOwner.Discount = Math.Round(saleBuyOwner.VetSaleBuyTrans.Sum(x => x.Discount.GetValueOrDefault()), 2, MidpointRounding.ToEven);
           

            try
            {
                await _saleBuyOwnerRepository.AddAsync(saleBuyOwner);
                await _uow.SaveChangesAsync(cancellationToken);

                saleBuyOwner.InvoiceNo = string.IsNullOrEmpty(request.InvoiceNo) ? ("#" + Convert.ToString(saleBuyOwner.RecordId)) : request.InvoiceNo;


                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }


        private decimal CalculateVatAmount(decimal _amount, decimal _quentity, decimal _ratio, bool _vatInclude)
        {
            decimal vatAmount = 0;
            try
            {
                //_vatInclude isaretli(true) ise KDV Dahil islemlerin yapılması gerekiyor
                if (_ratio != 0)
                {
                    decimal basePrice = 0;
                    if (_vatInclude)
                    {
                        basePrice = (_amount * _quentity) / (1 + (Convert.ToDecimal(_ratio) / 100));
                        vatAmount =  Math.Round((_amount * _quentity)- basePrice, 2, MidpointRounding.ToZero);
                    }
                    else
                    {
                        vatAmount = _ratio * (_amount * _quentity) / 100;
                    }
                }
            }
            catch (Exception )
            {
            }
            return vatAmount;
        }

    }

}
