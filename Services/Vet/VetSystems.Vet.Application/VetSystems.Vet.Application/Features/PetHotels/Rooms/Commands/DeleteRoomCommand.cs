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
using VetSystems.Vet.Application.Features.Vaccine.Commands;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.PetHotels.Rooms.Commands
{
    public class DeleteRoomCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeteleVaccineCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<VetRooms> _vetRoomsRepository;

        public DeleteRoomCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteRoomCommandHandler> logger,
           IIdentityRepository identityRepository, IRepository<VetRooms> vetRoomsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _vetRoomsRepository = vetRoomsRepository;
        }

        public async Task<Response<bool>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var _room = await _vetRoomsRepository.GetByIdAsync(request.Id);
                if (_room == null)
                {
                    _logger.LogWarning($"rooms update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("rooms update failed", 404);
                }
                _room.Deleted = true;
                _room.DeletedDate = DateTime.Now;
                _room.DeletedUsers = _identityRepository.Account.UserName;

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
