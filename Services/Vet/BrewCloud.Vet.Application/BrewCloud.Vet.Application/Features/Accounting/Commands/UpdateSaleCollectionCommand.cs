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
    public class UpdateSaleCollectionCommand : IRequest<Response<bool>>
    {
        public Guid SaleOwnerId { get; set; }
        public Guid CustomerId { get; set; }
        public int PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; } = string.Empty;
        public Guid? CollectionId { get; set; }
    }

    public class UpdateSaleCollectionCommandHandler : IRequestHandler<UpdateSaleCollectionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSaleCollectionCommandHandler> _logger;
        private readonly IRepository<VetPaymentCollection> _paymentCollectionRepository;

        public UpdateSaleCollectionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateSaleCollectionCommandHandler> logger, IRepository<VetPaymentCollection> paymentCollectionRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _paymentCollectionRepository = paymentCollectionRepository;
        }

        public async Task<Response<bool>> Handle(UpdateSaleCollectionCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var _collection = await _paymentCollectionRepository.FirstOrDefaultAsync(x => x.SaleBuyId == request.SaleOwnerId && x.Deleted == false);
                if (_collection != null)
                {
                    _collection.UpdateDate = DateTime.Now;
                    _collection.UpdateUsers = _identity.Account.UserName;
                    _collection.Date = request.Date;
                    _collection.Remark = request.Remark;
                    _collection.Credit = request.Amount;
                    _collection.Paid = request.Amount;
                    _collection.Debit = 0;
                    _collection.Total = request.Amount;
                    _collection.TotalPaid = request.Amount;
                    _collection.SaleBuyId = request.SaleOwnerId;
                    _collection.PaymetntId = request.PaymentId;
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
