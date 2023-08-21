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
    public class DeleteCasingDefinitionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCasingDefinitionCommandHandler : IRequestHandler<DeleteCasingDefinitionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCasingDefinitionCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.CasingDefinition> _casingdefinitionRepository;
        private readonly IIdentityRepository _identityRepository;

<<<<<<< Updated upstream
        public DeleteCasingDefinitionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteCasingDefinitionCommandHandler> logger, IRepository<Domain.Entities.CasingDefinition> casingdefinitionRepository, IIdentityRepository identityRepository)
=======
        public DeleteCasingDefinitionCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteCasingDefinitionCommandHandler> logger, IRepository<Domain.Entities.CasingDefinition> casingDefinitionRepository, IIdentityRepository identityRepository)
>>>>>>> Stashed changes
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
<<<<<<< Updated upstream
            _casingdefinitionRepository = casingdefinitionRepository ?? throw new ArgumentNullException(nameof(casingdefinitionRepository));
=======
            _casingdefinitionRepository = casingDefinitionRepository ?? throw new ArgumentNullException(nameof(casingDefinitionRepository));
>>>>>>> Stashed changes
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteCasingDefinitionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
<<<<<<< Updated upstream
                var casingdefinition = await _casingdefinitionRepository.GetByIdAsync(request.Id);
                if (casingdefinition == null)
=======
                var casingDefinition = await _casingdefinitionRepository.GetByIdAsync(request.Id);
                if (casingDefinition == null)
>>>>>>> Stashed changes
                {
                    _logger.LogWarning($"Casing update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

<<<<<<< Updated upstream
                casingdefinition.Deleted = true;
                casingdefinition.DeletedDate = DateTime.Now;
                casingdefinition.DeletedUsers = _identityRepository.Account.Email == null ? " " : _identityRepository.Account.Email;
=======
                casingDefinition.Deleted = true;
                casingDefinition.DeletedDate = DateTime.Now;
                casingDefinition.DeletedUsers = _identityRepository.Account.Email;
>>>>>>> Stashed changes

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
