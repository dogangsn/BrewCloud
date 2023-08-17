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
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.CustomerGroup.Commands
{
    public class DeleteCustomerGroupDefCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCustomerGroupDefCommandHandler : IRequestHandler<DeleteCustomerGroupDefCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.CustomerGroupDef> _customergroupdefRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteCustomerGroupDefCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerHandler> logger, IRepository<Domain.Entities.CustomerGroupDef> customergroupdefRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customergroupdefRepository = customergroupdefRepository ?? throw new ArgumentNullException(nameof(customergroupdefRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteCustomerGroupDefCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var customerGroupDef = await _customergroupdefRepository.GetByIdAsync(request.Id);
                if (customerGroupDef == null)
                {
                    _logger.LogWarning($"customerGroupDef deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                customerGroupDef.Deleted = true;
                customerGroupDef.DeletedDate = DateTime.Now;
                customerGroupDef.DeletedUsers = _identityRepository.Account.Email;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
