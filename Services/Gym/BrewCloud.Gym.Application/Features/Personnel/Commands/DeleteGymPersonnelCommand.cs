using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.Personnel.Commands
{
    public class DeleteGymPersonnelCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteGymPersonnelCommandHandler : IRequestHandler<DeleteGymPersonnelCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteGymPersonnelCommandHandler> _logger;
        private readonly IRepository<GymPersonnel> _personnelRepository;

        public DeleteGymPersonnelCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteGymPersonnelCommandHandler> logger, IRepository<GymPersonnel> personnelRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelRepository = personnelRepository ?? throw new ArgumentNullException(nameof(personnelRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<bool>> Handle(DeleteGymPersonnelCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                GymPersonnel personnel = await _personnelRepository.GetByIdAsync(request.Id);
                if (personnel != null)
                {
                    personnel.Deleted = true;

                    personnel.DeletedDate = DateTime.Now;
                    personnel.DeletedUsers = _identity.Account.UserName;
                }

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
