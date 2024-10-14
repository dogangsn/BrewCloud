using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Vaccine.Commands
{
    public class DeteleVaccineAppointmentCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeteleVaccineAppointmentCommandHandler : IRequestHandler<DeteleVaccineAppointmentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeteleVaccineAppointmentCommandHandler> _logger; 
        private readonly IIdentityRepository _identityRepository;

        private readonly IRepository<Vet.Domain.Entities.VetVaccineCalendar> _vetVaccineCalendarRepository;

        public DeteleVaccineAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeteleVaccineAppointmentCommandHandler> logger,
           IIdentityRepository identityRepository, IRepository<Domain.Entities.VetVaccineCalendar> vetVaccineCalendarRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _vetVaccineCalendarRepository = vetVaccineCalendarRepository;
        }

        public async Task<Response<bool>> Handle(DeteleVaccineAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var _vaccine = await _vetVaccineCalendarRepository.GetByIdAsync(request.Id);
                if (_vaccine == null)
                {
                    _logger.LogWarning($"Vaccine Appointment update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Vaccine Appointment update failed", 404);
                }
                _vaccine.Deleted = true;
                _vaccine.DeletedDate = DateTime.Now;
                _vaccine.DeletedUsers = _identityRepository.Account.UserName;

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
