using AutoMapper;
using BrewCloud.Gym.Application.Models;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.Personnel.Queries
{
    public class GetGymPersonnelListQuery : IRequest<Response<List<GymPersonnelDto>>>
    {
    }
    public class GetGymPersonnelListQueryHandler : IRequestHandler<GetGymPersonnelListQuery, Response<List<GymPersonnelDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGymPersonnelListQueryHandler> _logger;
        private readonly IRepository<GymPersonnel> _personnelRepository;

        public GetGymPersonnelListQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<GetGymPersonnelListQueryHandler> logger, IRepository<GymPersonnel> personnelRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelRepository = personnelRepository ?? throw new ArgumentNullException(nameof(personnelRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<List<GymPersonnelDto>>> Handle(GetGymPersonnelListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<GymPersonnelDto>>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true
            };
            try
            {
                var personnel = await _personnelRepository.GetAsync(x => x.Deleted == false);
                if (personnel != null)
                {
                    List<GymPersonnelDto> personnelList = _mapper.Map<List<GymPersonnelDto>>(personnel);
                    response.Data = personnelList;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
