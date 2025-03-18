using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.Personnel.Commands
{
    public class CreateGymPersonnelCommand : IRequest<Response<bool>>
    {
        public Guid BranchId { get; set; }
        public required string Name { get; set; }
        public required string SurName { get; set; }
        public required string PhoneNumber { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? Email { get; set; }
        public string? IdentityNumber { get; set; }
        public string? Graduate { get; set; }
        public string? Address { get; set; }
        public string? ImageId { get; set; }
    }

    public class CreateGymPersonnelCommandHandler : IRequestHandler<CreateGymPersonnelCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateGymPersonnelCommandHandler> _logger;
        private readonly IRepository<GymPersonnel> _personnelRepository;

        public CreateGymPersonnelCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateGymPersonnelCommandHandler> logger, IRepository<GymPersonnel> personnelRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelRepository = personnelRepository ?? throw new ArgumentNullException(nameof(personnelRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<bool>> Handle(CreateGymPersonnelCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                GymPersonnel personnel = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    SurName = request.SurName,
                    PhoneNumber = request.PhoneNumber,
                    PhoneNumber2 = request.PhoneNumber2,
                    Email = request.Email,
                    IdentityNumber = request.IdentityNumber,
                    Graduate = request.Graduate,
                    Address = request.Address,
                    ImageId = request.ImageId,
                    BranchId = request.BranchId,

                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };
                await _personnelRepository.AddAsync(personnel);
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
