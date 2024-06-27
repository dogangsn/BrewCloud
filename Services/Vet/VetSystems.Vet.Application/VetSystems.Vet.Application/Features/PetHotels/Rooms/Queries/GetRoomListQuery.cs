using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.PetHotels.Rooms;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.PetHotels.Rooms.Queries
{
    public class GetRoomListQuery : IRequest<Response<List<RoomListDto>>>
    {

    }

    public class GetRoomListQueryHandler : IRequestHandler<GetRoomListQuery, Response<List<RoomListDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetRooms> _vetRoomsRepository;

        public GetRoomListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetRooms> vetRoomsRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _vetRoomsRepository = vetRoomsRepository;
        }

        public async Task<Response<List<RoomListDto>>> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<RoomListDto>>.Success(200);
            try
            {
                List<VetRooms> _rooms = (await _vetRoomsRepository.GetAsync(x => x.Deleted == false)).ToList();
                var result = _mapper.Map<List<RoomListDto>>(_rooms.OrderByDescending(e => e.CreateDate));
                response.Data = result;
            }
            catch (Exception ex)
            {
                return Response<List<RoomListDto>>.Fail(ex.Message, 404);
            }
            return response;

        }
    }
}
