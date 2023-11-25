using AutoMapper;
using MassTransit.Futures.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class DeleteTileDefinationCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTileDefinationCommandHandler : IRequestHandler<DeleteRoleSettingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteTileDefinationCommandHandler> _logger;
        private readonly IRepository<TitleDefinitions> _titleRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteTileDefinationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteTileDefinationCommandHandler> logger, IRepository<TitleDefinitions> titleRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _titleRepository = titleRepository ?? throw new ArgumentNullException(nameof(titleRepository));
        }

        public async Task<Response<bool>> Handle(DeleteRoleSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var tile = await _titleRepository.GetByIdAsync(request.Id);
                if (tile == null)
                {
                    _logger.LogWarning($"Tile update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
                tile.Deleted = true;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;
        }
    }
}
