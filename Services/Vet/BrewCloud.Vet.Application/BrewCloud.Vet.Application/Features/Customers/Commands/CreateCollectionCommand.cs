using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Enums;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Features.Appointment.Commands;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Customers.Commands
{
    public class CreateCollectionCommand : IRequest<Response<string>>
    {
        public Guid CustomerId { get; set; }
        public Guid CollectionId { get; set; }
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }
        public decimal Ratio { get; set; }
        public bool? EnterAmount { get; set; }
        public Guid? Vaccineid { get; set; }
    }

    public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCollectionCommand> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IMediator _mediator;
        private readonly IRepository<Vet.Domain.Entities.VetProducts> _productRepository;
        private readonly IRepository<VetPaymentCollection> _paymentCollectionRepository;

        public CreateCollectionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCollectionCommand> logger, IRepository<Domain.Entities.VetSaleBuyOwner> saleBuyOwnerRepository, IMediator mediator, IRepository<VetProducts> productRepository, IRepository<VetPaymentCollection> paymentCollectionRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _saleBuyOwnerRepository = saleBuyOwnerRepository;
            _mediator = mediator;
            _productRepository = productRepository;
            _paymentCollectionRepository = paymentCollectionRepository;
        }

        public async Task<Response<string>> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = string.Empty
            };
            _uow.CreateTransaction(IsolationLevel.ReadCommitted);
            try
            {
                if (request.Amount == 0)
                {
                    return Response<string>.Fail("Tutar Bilgisi Olması Gerekmektedir.", 404);
                }
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;


                VetProducts _product = null;
                if (request.Vaccineid != Guid.Empty)
                {
                    _product = await _productRepository.GetByIdAsync(request.Vaccineid.GetValueOrDefault());
                    if (_product == null)
                    {
                        return Response<string>.Fail("Aşı Tanımı Bulunamadı.", 404);
                    }
                }

                #region VetSaleBuy
                Vet.Domain.Entities.VetSaleBuyOwner saleBuyOwner = new()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    CustomerId = request.CustomerId,
                    Type = (int)BuySaleType.Selling,
                    InvoiceNo = "#",
                    Total = request.Amount,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    PaymentType = request.PaymentType,
                    SupplierId = Guid.Empty,
                    demandsGuidId = Guid.Empty,
                    AppointmentId = request.CollectionId,
                    IsAppointment = true
                };
                saleBuyOwner.addSaleBuyTrans(new Vet.Domain.Entities.VetSaleBuyTrans
                {
                    Id = Guid.NewGuid(),
                    InvoiceNo = saleBuyOwner.InvoiceNo,
                    Ratio = _product != null ? _product.Ratio : request.Ratio,
                    ProductId = _product != null ? _product.Id : Guid.Empty,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Amount = request.Amount,
                    VatIncluded = false,
                    VatAmount = 0,
                    Price = request.EnterAmount.GetValueOrDefault() ? request.Amount : _product?.SellingPrice,
                    OwnerId = saleBuyOwner.Id,
                    NetPrice = 0,

                });
                #endregion

                #region PaymentCollection

                VetPaymentCollection paymentCollection = new()
                {
                    Id = Guid.NewGuid(),
                    CollectionId = request.CollectionId,
                    CustomerId = request.CustomerId,
                    Date = DateTime.Today,
                    Remark = "",
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Credit = 0,
                    Paid = request.Amount,
                    Debit = _product != null ? _product.SellingPrice - request.Amount : 0,
                    Total = request.Amount,
                    TotalPaid = request.Amount,
                    SaleBuyId = saleBuyOwner.Id
                };

                #endregion


                #region UpdatePaymentReceived
                var appointmentRecord = new UpdatePaymentReceivedAppointmentCommand()
                {
                    Id = request.CollectionId,
                    IsPaymentReceived = true,
                };
                var appointmentResponse = _mediator.Send(appointmentRecord); 
                #endregion

                try
                {
                    await _paymentCollectionRepository.AddAsync(paymentCollection);
                    await _saleBuyOwnerRepository.AddAsync(saleBuyOwner);
                    await _uow.SaveChangesAsync(cancellationToken);
                    saleBuyOwner.InvoiceNo = ("#" + Convert.ToString(saleBuyOwner.RecordId));
                    await _uow.SaveChangesAsync(cancellationToken);
                    _uow.Commit();
                }
                catch (Exception ex)
                {
                    _uow.Rollback();
                    response.IsSuccessful = false;
                }


            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.IsSuccessful = false;
                response.Data = ex.Message.ToString();
            }
            return response;
        }
    }
}
