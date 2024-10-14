using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.Shared.HubService;

namespace BrewCloud.Shared.Events
{
    public class RefreshAppointmentCalendarRequest : IntegrationBaseEvent, IRefreshAppointmentCalendarRequest
    {
        public Guid UserId { get; set; }
        public List<AppointmentCalendarDto> Appointments { get; set; }
    }
}
