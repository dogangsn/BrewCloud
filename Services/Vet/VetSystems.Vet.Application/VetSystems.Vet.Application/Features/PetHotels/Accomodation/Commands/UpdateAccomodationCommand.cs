using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Commands
{
    public class UpdateAccomodationCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Accomodation { get; set; }
        public string Remark { get; set; } = string.Empty;
    }

    public class UpdateAccomodationCommandHandler : IRequestHandler<UpdateAccomodationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAccomodationCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<VetAccomodation> _vetAccomodationRepository;

        public UpdateAccomodationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteAccomodationCommandHandler> logger, IIdentityRepository identityRepository, IRepository<VetAccomodation> vetAccomodationRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _identityRepository = identityRepository;
            _vetAccomodationRepository = vetAccomodationRepository;
        }

        public async Task<Response<bool>> Handle(UpdateAccomodationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = true
            };

            try
            {
                var _accomodation = await _vetAccomodationRepository.GetByIdAsync(request.Id);
                if (_accomodation == null)
                {
                    _logger.LogWarning($"rooms update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("rooms update failed", 404);
                }

                _accomodation.UpdateDate = DateTime.Now;
                _accomodation.UpdateUsers = _identity.Account.UserName;

                _accomodation.RoomId = request.RoomId;
                _accomodation.CustomerId = request.CustomerId;
                _accomodation.PatientsId = request.PatientsId;
                _accomodation.CheckinDate = request.CheckinDate;
                _accomodation.CheckOutDate = request.CheckOutDate;
                _accomodation.Accomodation = (AccomodationType)request.Accomodation;
                _accomodation.Remark = request.Remark;

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
