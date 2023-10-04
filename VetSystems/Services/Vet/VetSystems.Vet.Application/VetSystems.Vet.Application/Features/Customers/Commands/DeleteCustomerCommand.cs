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
using VetSystems.Vet.Application.Features.Store.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customersRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteCustomerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteCustomerCommandHandler> logger, IRepository<Domain.Entities.VetCustomers> customerRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customersRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var stores = await _customersRepository.GetByIdAsync(request.Id);
                if (stores == null)
                {
                    _logger.LogWarning($"Customer update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Customer update failed", 404);
                }

                stores.Deleted = true;
                stores.DeletedDate = DateTime.Now;
                stores.DeletedUsers = _identityRepository.Account.UserName;




                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }


}
