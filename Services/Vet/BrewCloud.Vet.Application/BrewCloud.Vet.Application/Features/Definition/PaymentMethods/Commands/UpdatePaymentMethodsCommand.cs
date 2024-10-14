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
using BrewCloud.Vet.Application.Features.Customers.Commands;
using BrewCloud.Vet.Application.Features.Definition.ProductCategory.Commands;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.PaymentMethods.Commands
{
    public class UpdatePaymentMethodsCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class UpdatePaymentMethodsCommandHandler : IRequestHandler<UpdatePaymentMethodsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePaymentMethodsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetPaymentMethods> _paymentMethodsRepository;

        public UpdatePaymentMethodsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdatePaymentMethodsCommandHandler> logger, IRepository<Domain.Entities.VetPaymentMethods> paymentMethodsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _paymentMethodsRepository = paymentMethodsRepository ?? throw new ArgumentNullException(nameof(paymentMethodsRepository));
        }

        public async Task<Response<bool>> Handle(UpdatePaymentMethodsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var paymentMethods = await _paymentMethodsRepository.GetByIdAsync(request.Id);
                if (paymentMethods == null)
                {
                    _logger.LogWarning($"Payment Methods update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                paymentMethods.Name = request.Name;
                paymentMethods.Remark = request.Remark;
                paymentMethods.UpdateDate = DateTime.Now;
                paymentMethods.UpdateUsers = _identity.Account.UserName;

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
