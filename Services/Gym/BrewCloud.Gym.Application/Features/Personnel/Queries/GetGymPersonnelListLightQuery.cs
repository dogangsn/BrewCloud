using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.Personnel.Queries
{
    public class GetGymPersonnelListLightQuery : IRequest<Response<List<IdNameDto>>>
    {
    }
    public class GetGymPersonnelListLightHandler : IRequestHandler<GetGymPersonnelListLightQuery, Response<List<IdNameDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGymPersonnelListLightHandler> _logger;
        private readonly IRepository<GymPersonnelPermission> _personnelPermissionRepository;
        private readonly IRepository<GymPersonnel> _personnelRepository;

        public GetGymPersonnelListLightHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<GetGymPersonnelListLightHandler> logger, IRepository<GymPersonnelPermission> personnelPermissionRepository, IRepository<GymPersonnel> personnelRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelPermissionRepository = personnelPermissionRepository ?? throw new ArgumentNullException(nameof(personnelPermissionRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _personnelRepository = personnelRepository;
        }

        public async Task<Response<List<IdNameDto>>> Handle(GetGymPersonnelListLightQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<IdNameDto>>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true
            };
            try
            {
                string query = @" select p.id, CONCAT(p.name, ' ', p.surname) AS name from gympersonnel p where p.deleted = 0 ";

                response.Data = _uow.Query<IdNameDto>(query);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
