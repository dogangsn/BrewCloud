using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Enums;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.SaleBuy.Commands
{
    public class UpdateSaleBuyCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
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

    public class UpdateSaleBuyCommandHandler : IRequestHandler<UpdateSaleBuyCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductDescriptionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _salebuyOwnerRepository;
        private readonly IRepository<Domain.Entities.VetSaleBuyTrans> _salebuyTransRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public UpdateSaleBuyCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateProductDescriptionCommandHandler> logger, IRepository<Domain.Entities.VetSaleBuyOwner> salebuyOwnerRepository, IIdentityRepository identityRepository, IRepository<Domain.Entities.VetTaxis> taxisRepository, IRepository<Domain.Entities.VetProducts> productRepository, IRepository<Domain.Entities.VetSaleBuyTrans> salebuyTransRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _salebuyOwnerRepository = salebuyOwnerRepository ?? throw new ArgumentNullException(nameof(salebuyOwnerRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _taxisRepository = taxisRepository ?? throw new ArgumentNullException(nameof(taxisRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _salebuyTransRepository = salebuyTransRepository ?? throw new ArgumentException(nameof(salebuyTransRepository));
        }

        public async Task<Response<bool>> Handle(UpdateSaleBuyCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                Vet.Domain.Entities.VetProducts _product = await _productRepository.GetByIdAsync(request.ProductId.GetValueOrDefault());
                if (_product == null)
                {
                    response.Data = false;
                    response.IsSuccessful = false;
                    return response;
                }

                var salebuyOwner = await _salebuyOwnerRepository.GetByIdAsync(request.OwnerId);
                if (salebuyOwner == null)
                {
                    _logger.LogWarning($"SaleBuyOwner update failed. Id number: {request.OwnerId}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                salebuyOwner.Date = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.Date), localTimeZone);
                salebuyOwner.CustomerId = request.CustomerId;
                salebuyOwner.Type = request.Type;
                salebuyOwner.SupplierId = request.SupplierId;
                //salebuyOwner.InvoiceNo = request.InvoiceNo; //Kurgulanacak
                salebuyOwner.Total = request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice;
                salebuyOwner.PaymentType = request.PaymentType;
                salebuyOwner.demandsGuidId = request.demandsGuidId;
                salebuyOwner.UpdateDate = DateTime.Now;
                salebuyOwner.UpdateUsers = _identity.Account.UserName;

                var taxis = await _taxisRepository.GetByIdAsync(_product.TaxisId.GetValueOrDefault());
                decimal vatAmaount = CalculateVatAmount((request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice), request.Amount, taxis.TaxRatio, (request.Type == (int)BuySaleType.Selling ? _product.SellingIncludeKDV.GetValueOrDefault() : _product.BuyingIncludeKDV.GetValueOrDefault()));


                var salebuyTrans = await _salebuyTransRepository.GetByIdAsync(request.Id);
                if (salebuyTrans == null)
                {
                    _logger.LogWarning($"SaleBuyOwner update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                salebuyTrans.ProductId = request.ProductId;
                salebuyTrans.UpdateDate = DateTime.Now;
                salebuyTrans.UpdateUsers = _identity.Account.UserName;
                salebuyTrans.Amount = request.Amount;
                salebuyTrans.VatIncluded = request.Type == (int)BuySaleType.Selling ? _product.SellingIncludeKDV : _product.BuyingIncludeKDV;
                salebuyTrans.VatAmount = vatAmaount;
                salebuyTrans.Price = request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice;
                salebuyTrans.NetPrice = (_product.SellingIncludeKDV.GetValueOrDefault() || _product.BuyingIncludeKDV.GetValueOrDefault()) ? (Math.Round((request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice) * request.Amount, 2, MidpointRounding.ToEven) - vatAmaount) : Math.Round((request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice) * request.Amount, 2, MidpointRounding.ToEven);

                 
                salebuyOwner.KDV = Math.Round(salebuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven);
                salebuyOwner.NetPrice = Math.Round(salebuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven);

                salebuyOwner.Total = Math.Round(salebuyOwner.VetSaleBuyTrans.Sum(x => x.NetPrice.GetValueOrDefault()), 2, MidpointRounding.ToEven)
                                            + Math.Round(salebuyOwner.VetSaleBuyTrans.Sum(x => x.VatAmount.GetValueOrDefault()), 2, MidpointRounding.ToEven);

                salebuyOwner.Discount = Math.Round(salebuyOwner.VetSaleBuyTrans.Sum(x => x.Discount.GetValueOrDefault()), 2, MidpointRounding.ToEven);


                await _uow.SaveChangesAsync(cancellationToken);


            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 405); 
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
