using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.HubService;
using VetSystems.Vet.Application.Models.Appointments;

namespace VetSystems.Vet.Application.Services.Hub
{
    public interface IHubService
    {
        Task<Response<bool>> SendRefreshAppointment(IRefreshAppointmentCalendarRequest request);
    }
}
