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

namespace BrewCloud.Vet.Application.Features.Definition.PaymentMethods.Commands
{
    public class CreatePaymentMethodsCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class CreatePaymentMethodsCommandHandler : IRequestHandler<CreatePaymentMethodsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePaymentMethodsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetPaymentMethods> _paymentMethodsRepository;

        public CreatePaymentMethodsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreatePaymentMethodsCommandHandler> logger, IRepository<Domain.Entities.VetPaymentMethods> paymentMethodsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _paymentMethodsRepository = paymentMethodsRepository ?? throw new ArgumentNullException(nameof(paymentMethodsRepository));
        }

        public async Task<Response<bool>> Handle(CreatePaymentMethodsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetPaymentMethods payment = new()
                {
                    Name = request.Name,
                    Remark = request.Remark,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };
                await _paymentMethodsRepository.AddAsync(payment);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
