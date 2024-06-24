using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Shared.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Commands
{
    public class UpdateCheckOutCommand : IRequest<Response<bool>>
    {
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal AccomodationAmount { get; set; }
        public decimal CollectionAmount { get; set; }
        public Guid PaymentId { get; set; }
    }

    public class UpdateCheckOutCommandHandler : IRequestHandler<UpdateCheckOutCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCheckOutCommandHandler> _logger;
        private readonly IRepository<VetAccomodation> _vetAccomodationRepository;
        private readonly IMediator _mediator;

        public UpdateCheckOutCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCheckOutCommandHandler> logger, IRepository<VetAccomodation> vetAccomodationRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetAccomodationRepository = vetAccomodationRepository;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(UpdateCheckOutCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {






            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
