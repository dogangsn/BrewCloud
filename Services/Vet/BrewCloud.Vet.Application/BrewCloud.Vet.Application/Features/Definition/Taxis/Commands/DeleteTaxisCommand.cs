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
using BrewCloud.Vet.Application.Features.Customers.Commands;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.Taxis.Commands
{
    public class DeleteTaxisCommand : IRequest<Response<bool>> 
    {
        public Guid Id { get; set; }
    }

    public class DeleteTaxisCommandHandler : IRequestHandler<DeleteTaxisCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteTaxisCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetTaxis> _taxisRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteTaxisCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteTaxisCommandHandler> logger, 
            IRepository<VetTaxis> taxisRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _taxisRepository = taxisRepository ?? throw new ArgumentNullException(nameof(taxisRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteTaxisCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var taxis = await _taxisRepository.GetByIdAsync(request.Id);
                if (taxis == null)
                {
                    _logger.LogWarning($"taxis deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                taxis.Deleted = true;
                taxis.DeletedDate = DateTime.Now;
                taxis.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
