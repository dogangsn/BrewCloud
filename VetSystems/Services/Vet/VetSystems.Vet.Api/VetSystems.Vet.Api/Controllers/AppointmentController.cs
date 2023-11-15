using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using VetSystems.Vet.Application.Features.Appointments.Commands;
//using VetSystems.Vet.Application.Features.Appointments.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    public class AppointmentController
    {
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class AppointmentsController : ControllerBase
        {
            private readonly IMediator _mediator;
            public AppointmentsController(IMediator mediator)
            {
                _mediator = mediator;
            }

            //[HttpGet(Name = "AppointmentsList")]
            //public async Task<IActionResult> AppointmentsList()
            //{
            //    var command = new AppointmentsListQuery();
            //    var result = await _mediator.Send(command);
            //    return Ok(result);
            //}

            //[HttpPost(Name = "CreateAppointment")]
            //public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
            //{
            //    var result = await _mediator.Send(command);
            //    return Ok(result);
            //}

            //[HttpPost(Name = "DeleteAppointment")]
            //public async Task<IActionResult> DeleteAppointment([FromBody] DeleteAppointmentCommand command)
            //{
            //    var result = await _mediator.Send(command);
            //    return Ok(result);
            //}

            //[HttpPost(Name = "UpdateAppointmentById")]
            //[AllowAnonymous]
            //public async Task<IActionResult> UpdateAppointmentById([FromBody] UpdateAppointmentCommand model)
            //{
            //    var result = await _mediator.Send(model);
            //    return Ok(result);
            //}

        }
    }
}
