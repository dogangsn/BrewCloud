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
using VetSystems.Vet.Application.Features.Appointment.Commands;
using VetSystems.Vet.Application.Features.Store.Commands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class DeleteShortCutsCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteShortCutsCommandHandler : IRequestHandler<DeleteShortCutsCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteShortCutsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetShortcut> _shortCutRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteShortCutsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteShortCutsCommandHandler> logger,  IIdentityRepository identityRepository, IRepository<Vet.Domain.Entities.VetShortcut> shortCutRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _shortCutRepository = shortCutRepository ?? throw new ArgumentNullException(nameof(shortCutRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<string>> Handle(DeleteShortCutsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                Data = string.Empty,
                IsSuccessful = true
            };
            try
            {
                var shortcut = await _shortCutRepository.GetByIdAsync(request.Id);
                if (shortcut == null)
                {
                    _logger.LogWarning($"Shortcut update failed. Id number: {request.Id}");
                    return Response<string>.Fail("Shortcut update failed", 404);
                }

                shortcut.Deleted = true;
                shortcut.DeletedDate = DateTime.Now;
                shortcut.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }


}
