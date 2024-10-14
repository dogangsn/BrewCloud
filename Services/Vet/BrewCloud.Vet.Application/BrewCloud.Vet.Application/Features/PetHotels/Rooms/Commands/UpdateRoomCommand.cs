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
using BrewCloud.Vet.Application.Features.Vaccine.Commands;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.PetHotels.Rooms.Commands
{
    public class UpdateRoomCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public RoomRriceType PricingType { get; set; }
    }

    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeteleVaccineCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<VetRooms> _vetRoomsRepository;

        public UpdateRoomCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateRoomCommandHandler> logger,
           IIdentityRepository identityRepository, IRepository<VetRooms> vetRoomsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _vetRoomsRepository = vetRoomsRepository;
        }

        public async Task<Response<bool>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
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
                _room.UpdateDate = DateTime.UtcNow;
                _room.RoomName = request.RoomName;
                _room.Price = request.Price;
                _room.PricingType = request.PricingType;


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
