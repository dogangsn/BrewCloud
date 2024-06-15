using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;


namespace VetSystems.Vet.Application.Features.Accounting.Commands
{
    public class CreateBalanceSaleCollectionCommand : IRequest<Response<bool>>
    {
        public Guid CustomerId { get; set; }
        public int PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; } = string.Empty;
    }

    public class CreateBalanceSaleCollectionCommandHandler : IRequestHandler<CreateBalanceSaleCollectionCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBalanceSaleCollectionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<VetPaymentCollection> _paymentCollectionRepository;
        private readonly IMediator _mediator;

        public CreateBalanceSaleCollectionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateBalanceSaleCollectionCommandHandler> logger, IRepository<VetSaleBuyOwner> saleBuyOwnerRepository, IRepository<VetPaymentCollection> paymentCollectionRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _saleBuyOwnerRepository = saleBuyOwnerRepository;
            _paymentCollectionRepository = paymentCollectionRepository;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CreateBalanceSaleCollectionCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                var paymentcollection = (await _paymentCollectionRepository.GetAsync(x => x.CustomerId == request.CustomerId && x.Deleted == false)).ToList();
                var paymentCollectionSaleBuyIds = paymentcollection.Select(p => p.SaleBuyId).ToHashSet();

                List<VetSaleBuyOwner> vetSaleBuys = (await _saleBuyOwnerRepository.GetAsync(x => x.CustomerId == request.CustomerId && x.Deleted == false && !paymentCollectionSaleBuyIds.Contains(x.Id))).OrderBy(x => x.CreateDate).ToList();

                if (vetSaleBuys.Count > 0)
                {
                    decimal _amount = request.Amount;
                    foreach (var item in vetSaleBuys)
                    {
                        decimal _paidtotal = 0;
                        if (_amount == 0)
                            continue;

                        if (item.Total.GetValueOrDefault() <= _amount)
                            _paidtotal = item.Total.GetValueOrDefault();
                        else
                            _paidtotal = _amount;

                        SendCreateCollection(request, item.Id, _paidtotal);

                        _amount = (decimal)_amount - (decimal)item.Total.GetValueOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;
        }

        public void SendCreateCollection(CreateBalanceSaleCollectionCommand filter, Guid saleBuyId, decimal amount)
        {
            var req = new CreateSaleCollectionCommand
            {
                CustomerId = filter.CustomerId,
                Date = filter.Date,
                PaymentId = filter.PaymentId,
                Remark = filter.Remark,
                Amount = amount,
                SaleOwnerId = saleBuyId
            };
            _mediator.Send(req);
        }

    }
}
