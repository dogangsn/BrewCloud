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
using BrewCloud.Vet.Application.Models.FileManager;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Appointment.Queries
{
    public class AppointmentDateCheckControlQuery : IRequest<Response<bool>>
    {
        public DateTime Date { get; set; }
    }

    public class AppointmentDateCheckControlQueryHandler : IRequestHandler<AppointmentDateCheckControlQuery, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentDateCheckControlQueryHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _AppointmentRepository;

        public AppointmentDateCheckControlQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<AppointmentDateCheckControlQueryHandler> logger, IRepository<VetAppointments> appointmentRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _AppointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(AppointmentDateCheckControlQuery request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                var _appointments = await _AppointmentRepository.FirstOrDefaultAsync(x => x.BeginDate == TimeZoneInfo.ConvertTimeFromUtc(request.Date, localTimeZone) && x.Deleted == false);
                if (_appointments != null)
                {
                    response.Data = false;
                }
                else
                {
                    response.Data = true;
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
