using AutoMapper;
using BrewCloud.Gym.Domain.Contracts;
using BrewCloud.Gym.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Enums;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Gym.Application.Features.Member.Commands
{
    public class CreateGymMemberCommand : IRequest<Response<bool>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? IdentityNumber { get; set; }
        public bool IsMember { get; set; }
        public bool IsMaried { get; set; }
        public string? District { get; set; }
        public string? Job { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public BloopType? BloopType { get; set; }
        public string? EmergencyPerson { get; set; }
        public string? EmergencyPersonPhone { get; set; }
        public string? Note { get; set; }
        public string? CardNumber { get; set; }
    }

    public class CreateGymMemberCommandHandler : IRequestHandler<CreateGymMemberCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateGymMemberCommandHandler> _logger;
        private readonly IRepository<GymMember> _memberRepository;

        public CreateGymMemberCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateGymMemberCommandHandler> logger, IRepository<GymMember> memberRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<bool>> Handle(CreateGymMemberCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                GymMember member = new()
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IdentityNumber = request.IdentityNumber,
                    IsMember = request.IsMember,
                    IsMaried = request.IsMaried,
                    District = request.District,
                    Job = request.Job,
                    Phone = request.Phone,
                    Email = request.Email,
                    Address = request.Address,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender != null ? (byte)request.Gender : null,
                    BloopType = request.BloopType != null ? (byte)request.BloopType : null,
                    EmergencyPerson = request.EmergencyPerson,
                    EmergencyPersonPhone = request.EmergencyPersonPhone,
                    Note = request.Note,
                    CardNumber = request.CardNumber,
                    //BranchId = _identity -- bu alan doldurulacak.
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };
                await _memberRepository.AddAsync(member);
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
