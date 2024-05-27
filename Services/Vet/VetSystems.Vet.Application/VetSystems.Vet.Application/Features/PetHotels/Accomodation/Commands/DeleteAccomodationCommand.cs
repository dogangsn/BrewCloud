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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Commands
{
    public class DeleteAccomodationCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAccomodationCommandHandler : IRequestHandler<DeleteAccomodationCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAccomodationCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<VetAccomodation> _vetAccomodationRepository;

        public DeleteAccomodationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteAccomodationCommandHandler> logger, IIdentityRepository identityRepository, IRepository<VetAccomodation> vetAccomodationRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _identityRepository = identityRepository;
            _vetAccomodationRepository = vetAccomodationRepository;
        }

        public async Task<Response<bool>> Handle(DeleteAccomodationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };

            try
            {
                var _accomodation = await _vetAccomodationRepository.GetByIdAsync(request.Id);
                if (_accomodation == null)
                {
                    _logger.LogWarning($"rooms update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("rooms update failed", 404);
                }
                _accomodation.Deleted = true;
                _accomodation.DeletedDate = DateTime.Now;
                _accomodation.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                return Response<bool>.Fail("rooms update failed", 404);
            }

            return response;




        }
    }
}
