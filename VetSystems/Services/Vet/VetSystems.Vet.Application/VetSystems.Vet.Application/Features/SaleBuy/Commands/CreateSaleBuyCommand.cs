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
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.SaleBuy.Commands
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
    }

    public class CreateSaleBuyCommandHandler : IRequestHandler<CreateSaleBuyCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;

        public CreateSaleBuyCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.VetSaleBuyOwner> salebuyownerRepository, IRepository<Vet.Domain.Entities.VetProducts> productRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _saleBuyOwnerRepository = salebuyownerRepository ?? throw new ArgumentNullException(nameof(salebuyownerRepository));
            _productRepository = productRepository;
        }
        public async Task<Response<bool>> Handle(CreateSaleBuyCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

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
                Date = request.Date,
                CustomerId = request.CustomerId,
                Type = request.Type,
                SupplierId = request.SupplierId,
                InvoiceNo = "#",
                Total = request.Type == (int)BuySaleType.Selling ? _product.SellingPrice : _product.BuyingPrice,
                CreateDate = DateTime.Now,
                CreateUsers = _identity.Account.UserName,
                PaymentType = request.PaymentType,
            };

            saleBuyOwner.addSaleBuyTrans(new Vet.Domain.Entities.VetSaleBuyTrans
            {
                Id = Guid.NewGuid(),
                InvoiceNo = saleBuyOwner.InvoiceNo,
                Ratio = _product.Ratio,
                ProductId = request.ProductId.GetValueOrDefault(),
                CreateDate = DateTime.Now,
                CreateUsers = _identity.Account.UserName
            });

            try
            {
                await _saleBuyOwnerRepository.AddAsync(saleBuyOwner);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
