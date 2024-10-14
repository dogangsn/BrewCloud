using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.HubService;
using BrewCloud.Vet.Application.Models.Appointments;

namespace BrewCloud.Vet.Application.Services.Hub
{
    public interface IHubService
    {
        Task<Response<bool>> SendRefreshAppointment(IRefreshAppointmentCalendarRequest request);
    }
}
