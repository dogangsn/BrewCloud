﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Shared.Service;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Commands
{
    public class CreateAccomodationCommand : IRequest<Response<bool>>
    {
        public int Type { get; set; }
        public Guid RoomId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Accomodation { get; set; }
        public string Remark { get; set; } = string.Empty;
    }

    public class CreateAccomodationCommandHandler : IRequestHandler<CreateAccomodationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAccomodationCommandHandler> _logger;
        private readonly IRepository<VetAccomodation> _vetAccomodationRepository;
        private readonly IMediator _mediator;

        public CreateAccomodationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAccomodationCommandHandler> logger, IRepository<VetAccomodation> vetAccomodationRepository, IMediator mediator)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetAccomodationRepository = vetAccomodationRepository;
            _mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CreateAccomodationCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = true
            };
            _uow.CreateTransaction(IsolationLevel.ReadCommitted);
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            try
            {
                VetAccomodation accomodation = new()
                {
                    Type = request.Type,
                    RoomId = request.RoomId,
                    CustomerId = request.CustomerId,
                    PatientsId = request.PatientsId,
                    CheckinDate = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.CheckinDate), localTimeZone),
                    CheckOutDate = TimeZoneInfo.ConvertTime(Convert.ToDateTime(request.CheckOutDate), localTimeZone),
                    Accomodation = (AccomodationType)request.Accomodation,
                    Remark = request.Remark,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };

                await _vetAccomodationRepository.AddAsync(accomodation);

                await _uow.SaveChangesAsync(cancellationToken);
                _uow.Commit();
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }

            return response;
        }
    }
}
