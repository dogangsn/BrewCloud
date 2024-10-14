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
using BrewCloud.Vet.Application.Features.Definition.ProductCategory.Commands;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.PaymentMethods.Commands
{
    public class DeletePaymentMethodsCommand : IRequest<Response<bool>>
    {
        public int RecId { get; set; }
    }

    public class DeletePaymentMethodsCommandHandler : IRequestHandler<DeletePaymentMethodsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePaymentMethodsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetPaymentMethods> _paymentMethodsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeletePaymentMethodsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeletePaymentMethodsCommandHandler> logger, IRepository<Domain.Entities.VetPaymentMethods>  paymentMethodsRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _paymentMethodsRepository = paymentMethodsRepository ?? throw new ArgumentNullException(nameof(paymentMethodsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeletePaymentMethodsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var paymentMethods = await _paymentMethodsRepository.FirstOrDefaultAsync(x=> x.RecId == request.RecId);
                if (paymentMethods == null)
                {
                    _logger.LogWarning($"Paymet Methots update failed. Id number: {request.RecId}");
                    return Response<bool>.Fail("Paymet Methods update failed", 404);
                }

                paymentMethods.Deleted = true;
                paymentMethods.DeletedDate = DateTime.Now;
                paymentMethods.DeletedUsers = _identityRepository.Account.UserName;

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
