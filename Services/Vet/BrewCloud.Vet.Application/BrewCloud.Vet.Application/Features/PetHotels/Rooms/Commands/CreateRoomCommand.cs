using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Shared.Service;
using AutoMapper;
using System.Data;

namespace BrewCloud.Vet.Application.Features.PetHotels.Rooms.Commands
{
    public class CreateRoomCommand : IRequest<Response<bool>>
    {
        public string RoomName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public RoomRriceType PricingType { get; set; }
    }

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomCommandHandler> _logger;
        private readonly IRepository<VetRooms> _vetRoomsRepository;
        private readonly IMediator _mediator;

        public CreateRoomCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateRoomCommandHandler> logger, IRepository<VetRooms> vetRoomsRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetRoomsRepository = vetRoomsRepository;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = true
            };
            _uow.CreateTransaction(IsolationLevel.ReadCommitted);
            try
            {
                VetRooms room = new()
                {
                    Id = Guid.NewGuid(), 
                    CreateDate = DateTime.Now,
                    RoomName = request.RoomName,
                    Price = request.Price,
                    PricingType = request.PricingType
                };
                await _vetRoomsRepository.AddAsync(room);
                await _uow.SaveChangesAsync(cancellationToken);
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;



        }
    }
}
