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

namespace BrewCloud.Vet.Application.Features.Definition.CustomerGroup.Commands
{
    public class CreateCustomerGroupDefCommand : IRequest<Response<bool>>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class CreateCustomerGroupDefCommandHandler : IRequestHandler<CreateCustomerGroupDefCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerGroupDefCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomerGroupDef> _customergroupdefRepository;

        public CreateCustomerGroupDefCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCustomerGroupDefCommandHandler> logger, IRepository<Domain.Entities.VetCustomerGroupDef> customergroupdefRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customergroupdefRepository = customergroupdefRepository ?? throw new ArgumentNullException(nameof(customergroupdefRepository));
        }

        public async Task<Response<bool>> Handle(CreateCustomerGroupDefCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetCustomerGroupDef customerGroupDef = new()
                {
                    Id = Guid.NewGuid(),
                    Code = request.Code,
                    Name = request.Name,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };
                await _customergroupdefRepository.AddAsync(customerGroupDef);
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
