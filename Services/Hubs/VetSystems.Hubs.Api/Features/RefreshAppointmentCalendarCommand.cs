using MediatR;
using Microsoft.AspNetCore.SignalR;
using VetSystems.Hubs.Api.Hubs;
using VetSystems.Hubs.Api.Models;
using VetSystems.Shared.Dtos;

namespace VetSystems.Hubs.Api.Features
{
    public class RefreshAppointmentCalendarCommand : IRequest<Response<List<AppointmentCalendarDto>>>
    {
        public Guid UserId { get; set; }
        public List<AppointmentCalendarDto> Appointments { get; set; }
    }

    public class RefreshAppointmentCalendarCommandHandler : IRequestHandler<RefreshAppointmentCalendarCommand, Response<List<AppointmentCalendarDto>>>
    {
        private IHubContext<ServiceHub> _hub;
        private readonly ILogger<RefreshAppointmentCalendarCommandHandler> _logger;

        public RefreshAppointmentCalendarCommandHandler(IHubContext<ServiceHub> hub, ILogger<RefreshAppointmentCalendarCommandHandler> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public async Task<Response<List<AppointmentCalendarDto>>> Handle(RefreshAppointmentCalendarCommand request, CancellationToken cancellationToken)
        {

            var response = Response<List<AppointmentCalendarDto>>.Success(200);
            try
            {
                await _hub.Clients.Group(request.UserId.ToString()).SendAsync("refreshappointmentcalendar", request);
            }
            catch (Exception ex)
            {
                return Response<List<AppointmentCalendarDto>>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
