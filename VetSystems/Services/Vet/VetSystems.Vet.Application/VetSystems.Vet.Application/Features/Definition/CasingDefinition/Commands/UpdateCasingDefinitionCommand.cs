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

namespace VetSystems.Vet.Application.Features.Definition.CasingDefinition.Commands
{
    public class UpdateCasingDefinitionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string CaseName { get; set; } = string.Empty;

        public bool Durumu { get; set; }
    }

    public class UpdateCasingDefinitionCommandHandler : IRequestHandler<UpdateCasingDefinitionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCasingDefinitionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.CasingDefinition> _casingdefinitionRepository;

        public UpdateCasingDefinitionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCasingDefinitionCommandHandler> logger, IRepository<Domain.Entities.CasingDefinition> casingdefinitionRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _casingdefinitionRepository = casingdefinitionRepository ?? throw new ArgumentNullException(nameof(casingdefinitionRepository));
        }

        public async Task<Response<bool>> Handle(UpdateCasingDefinitionCommand request, CancellationToken cancellationToken)
         {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var casingDefinitions = await _casingdefinitionRepository.GetByIdAsync(request.Id);
                if (casingDefinitions == null)
                {
                    _logger.LogWarning($"Casing update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                casingDefinitions.CaseName = request.CaseName;
                casingDefinitions.Active = request.Durumu;
                casingDefinitions.UpdateDate = DateTime.Now;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
