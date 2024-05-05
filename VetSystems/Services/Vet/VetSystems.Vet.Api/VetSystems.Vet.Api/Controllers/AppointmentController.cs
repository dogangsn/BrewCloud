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

        [HttpPost(Name = "AppointmentsList")]
        public async Task<IActionResult> AppointmentsList([FromBody] AppointmentsListQuery command)
        { 
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

        [HttpPost(Name = "UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost(Name = "DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment([FromBody] DeleteAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdatePaymentReceivedAppointment")]
        public async Task<IActionResult> UpdatePaymentReceivedAppointment([FromBody] UpdatePaymentReceivedAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateCompletedAppointment")]
        public async Task<IActionResult> UpdateCompletedAppointment([FromBody] UpdateCompletedAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
