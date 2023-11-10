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
using VetSystems.Vet.Application.Features.Definition.CustomerGroup.Commands;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<Response<bool>>
    {
        public CustomerDetailsDto CustomerDetailsDto { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customersRepository;

        public UpdateCustomerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCustomerCommandHandler> logger, IRepository<Domain.Entities.VetCustomers> customersRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customersRepository = customersRepository ?? throw new ArgumentNullException(nameof(customersRepository));
        }

        public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                //var customerGroupDef = await _customerGroupDefRepository.GetByIdAsync(request.Id);
                //if (customerGroupDef == null)
                //{
                //    _logger.LogWarning($"customerGroupDef update failed. Id number: {request.Id}");
                //    return Response<bool>.Fail("Store update failed", 404);
                //}
                //customerGroupDef.Code = request.Code;
                //customerGroupDef.Name = request.Name;
                //customerGroupDef.UpdateDate = DateTime.Now;
                //customerGroupDef.UpdateUsers = _identity.Account.UserName;
                //await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }


}
