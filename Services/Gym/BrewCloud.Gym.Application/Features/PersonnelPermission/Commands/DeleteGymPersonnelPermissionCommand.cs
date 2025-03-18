using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.PersonnelPermission.Commands
{
    public class DeleteGymPersonnelPermissionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteGymPersonnelPermissionHandler : IRequestHandler<DeleteGymPersonnelPermissionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteGymPersonnelPermissionHandler> _logger;
        private readonly IRepository<GymPersonnelPermission> _personnelPermissionRepository;
        public DeleteGymPersonnelPermissionHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteGymPersonnelPermissionHandler> logger, IRepository<GymPersonnelPermission> personnelPermissionRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelPermissionRepository = personnelPermissionRepository ?? throw new ArgumentNullException(nameof(personnelPermissionRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }
        public async Task<Response<bool>> Handle(DeleteGymPersonnelPermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                GymPersonnelPermission personnel = await _personnelPermissionRepository.GetByIdAsync(request.Id);
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
