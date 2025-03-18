using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Gym.Domain.Enums;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.PersonnelPermission.Commands
{
    public class UpdateGymPersonnelPermissionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; } 
        public PermissionType PermissionType { get; set; }
        public bool IsApproved { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Note { get; set; }
    }

    public class UpdateGymPersonnelPermissionHandler : IRequestHandler<UpdateGymPersonnelPermissionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateGymPersonnelPermissionHandler> _logger;
        private readonly IRepository<GymPersonnelPermission> _personnelPermissionRepository;

        public UpdateGymPersonnelPermissionHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateGymPersonnelPermissionHandler> logger, IRepository<GymPersonnelPermission> personnelPermissionRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelPermissionRepository = personnelPermissionRepository ?? throw new ArgumentNullException(nameof(personnelPermissionRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<bool>> Handle(UpdateGymPersonnelPermissionCommand request, CancellationToken cancellationToken)
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
                    personnel.PermissionType = request.PermissionType;
                    personnel.IsApproved = request.IsApproved;
                    personnel.BeginDate = request.BeginDate;
                    personnel.EndDate = request.EndDate;
                    personnel.Note = request.Note;

                    personnel.UpdateDate = DateTime.Now;
                    personnel.UpdateUsers = _identity.Account.UserName;
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
