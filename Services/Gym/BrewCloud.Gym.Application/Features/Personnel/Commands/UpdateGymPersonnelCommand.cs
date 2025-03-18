using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.Personnel.Commands
{
    public class UpdateGymPersonnelCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
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

    public class UpdateGymPersonnelCommandHandler : IRequestHandler<UpdateGymPersonnelCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateGymPersonnelCommandHandler> _logger;
        private readonly IRepository<GymPersonnel> _personnelRepository;

        public UpdateGymPersonnelCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateGymPersonnelCommandHandler> logger, IRepository<GymPersonnel> personnelRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personnelRepository = personnelRepository ?? throw new ArgumentNullException(nameof(personnelRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<bool>> Handle(UpdateGymPersonnelCommand request, CancellationToken cancellationToken)
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
                    personnel.Name = request.Name;
                    personnel.SurName = request.SurName;
                    personnel.PhoneNumber = request.PhoneNumber;
                    personnel.PhoneNumber2 = request.PhoneNumber2;
                    personnel.Email = request.Email;
                    personnel.IdentityNumber = request.IdentityNumber;
                    personnel.Graduate = request.Graduate;
                    personnel.Address = request.Address;
                    personnel.ImageId = request.ImageId;
                    personnel.BranchId = request.BranchId;

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
