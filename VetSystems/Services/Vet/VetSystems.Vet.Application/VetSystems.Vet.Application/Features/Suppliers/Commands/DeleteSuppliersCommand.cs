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

namespace VetSystems.Vet.Application.Features.Suppliers.Commands
{
    public class DeleteSuppliersCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSuppliersCommandHandler : IRequestHandler<DeleteSuppliersCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteSuppliersCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.Suppliers> _suppliersRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteSuppliersCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteSuppliersCommandHandler> logger, IRepository<Domain.Entities.Suppliers> suppliersRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _suppliersRepository = suppliersRepository ?? throw new ArgumentNullException(nameof(suppliersRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteSuppliersCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var casingdefinition = await _suppliersRepository.GetByIdAsync(request.Id);
                if (casingdefinition == null)
                {
                    _logger.LogWarning($"Suppliers update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                casingdefinition.Deleted = true;
                casingdefinition.DeletedDate = DateTime.Now;
                casingdefinition.DeletedUsers = _identityRepository.Account.Email;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }
}
