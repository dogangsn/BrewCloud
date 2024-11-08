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
    public class CreateGymPersonnelPermissionCommand : IRequest<Response<bool>>
    {
        public Guid PersonnelId { get; set; }
        public PermissionType PermissionType { get; set; }
        public bool IsApproved { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Note { get; set; }
    }

    public class CreateGymPersonnelPermissionHandler : IRequestHandler<CreateGymPersonnelPermissionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateGymPersonnelPermissionHandler> _logger;
        private readonly IRepository<GymPersonnelPermission> _personnelPermissionRepository;

        public CreateGymPersonnelPermissionHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateGymPersonnelPermissionHandler> logger, IRepository<GymPersonnelPermission> personnelPermissionRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelPermissionRepository = personnelPermissionRepository ?? throw new ArgumentNullException(nameof(personnelPermissionRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<bool>> Handle(CreateGymPersonnelPermissionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                GymPersonnelPermission personnel = new()
                {
                    Id = Guid.NewGuid(),
                    IsApproved = request.IsApproved,
                    PersonnelId = request.PersonnelId,
                    PermissionType = request.PermissionType,
                    BeginDate = request.BeginDate,
                    EndDate = request.EndDate,
                    Note = request.Note,

                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };
                await _personnelPermissionRepository.AddAsync(personnel);
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
