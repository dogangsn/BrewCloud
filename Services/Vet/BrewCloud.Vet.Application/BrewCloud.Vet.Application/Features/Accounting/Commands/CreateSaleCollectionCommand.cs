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
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Accounting.Commands
{
    public class CreateSaleCollectionCommand : IRequest<Response<bool>>
    {
        public Guid SaleOwnerId { get; set; }
        public Guid CustomerId { get; set; }
        public int PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; } = string.Empty;
        public Guid? CollectionId { get; set; }
    }

    public class CreateSaleCollectionCommandHandler : IRequestHandler<CreateSaleCollectionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSaleCollectionCommandHandler> _logger;
        private readonly IRepository<VetPaymentCollection> _paymentCollectionRepository;

        public CreateSaleCollectionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSaleCollectionCommandHandler> logger, IRepository<VetPaymentCollection> paymentCollectionRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _paymentCollectionRepository = paymentCollectionRepository;
        }

        public async Task<Response<bool>> Handle(CreateSaleCollectionCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {

                var _collection = await _paymentCollectionRepository.FirstOrDefaultAsync(x=> x.SaleBuyId == request.SaleOwnerId && x.Deleted == false);
                if (_collection != null)
                {
                    _collection.UpdateDate = DateTime.Now;
                    _collection.UpdateUsers = _identity.Account.UserName;
                    _collection.Date = request.Date;
                    _collection.Remark = request.Remark;
                    _collection.Credit = request.Amount + _collection.Credit;
                    _collection.Paid = request.Amount + _collection.Paid;
                    _collection.Debit = 0;
                    _collection.Total = request.Amount + _collection.Total;
                    _collection.TotalPaid = request.Amount + _collection.TotalPaid;
                    _collection.SaleBuyId = request.SaleOwnerId;
                    _collection.PaymetntId = request.PaymentId;

                }
                else
                {
                    VetPaymentCollection paymentCollection = new()
                    {
                        Id = Guid.NewGuid(),
                        CreateDate = DateTime.Now,
                        CreateUsers = _identity.Account.UserName,
                        CustomerId = request.CustomerId,
                        Date = request.Date,
                        Remark = request.Remark,
                        Credit = request.Amount,
                        Paid = request.Amount,
                        Debit = 0,
                        Total = request.Amount,
                        TotalPaid = request.Amount,
                        SaleBuyId = request.SaleOwnerId,
                        PaymetntId = request.PaymentId
                    };
                    await _paymentCollectionRepository.AddAsync(paymentCollection);
                }
                await _uow.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
