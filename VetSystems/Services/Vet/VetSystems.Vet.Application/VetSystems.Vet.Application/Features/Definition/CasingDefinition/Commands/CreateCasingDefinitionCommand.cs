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

namespace VetSystems.Vet.Application.Features.Definition.CasingDefinition.Commands
{

    public class CreateCasingDefinitionCommand : IRequest<Response<bool>>
    {
        public string CaseName { get; set; } = string.Empty;

        public bool Active { get; set; }

    }

    public class CreateCasingDefinitionCommandHandler : IRequestHandler<CreateCasingDefinitionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCasingDefinitionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCasingDefinition> _casingdefinitionRepository;

        public CreateCasingDefinitionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateCasingDefinitionCommandHandler> logger, IRepository<Domain.Entities.VetCasingDefinition> casingdefinitionRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _casingdefinitionRepository = casingdefinitionRepository ?? throw new ArgumentNullException(nameof(casingdefinitionRepository));
        }

        public async Task<Response<bool>> Handle(CreateCasingDefinitionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetCasingDefinition casingDefinition = new()
                {
                    Id = Guid.NewGuid(),
                    CaseName = request.CaseName,
                    Active = request.Active,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };
                await _casingdefinitionRepository.AddAsync(casingDefinition);
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
