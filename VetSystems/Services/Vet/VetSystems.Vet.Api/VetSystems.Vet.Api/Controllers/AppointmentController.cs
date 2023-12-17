using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Appointment.Commands;
using VetSystems.Vet.Application.Features.Appointment.Queries;
using VetSystems.Vet.Application.Features.Customers.Queries;
//using VetSystems.Vet.Application.Features.Appointments.Commands;
//using VetSystems.Vet.Application.Features.Appointments.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "AppointmentsList")]
        public async Task<IActionResult> AppointmentsList()
        {
            var command = new AppointmentsListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "AppointmentFindByIdList")]
        public async Task<IActionResult> AppointmentFindByIdList([FromBody] AppointmentFindByIdListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        } 

    }
}
