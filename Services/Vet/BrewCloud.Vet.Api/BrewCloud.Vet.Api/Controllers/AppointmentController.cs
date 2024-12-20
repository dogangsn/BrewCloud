﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Agenda.Queries;
using BrewCloud.Vet.Application.Features.Appointment.Commands;
using BrewCloud.Vet.Application.Features.Appointment.Queries;
using BrewCloud.Vet.Application.Features.Customers.Queries;
//using BrewCloud.Vet.Application.Features.Appointments.Commands;
//using BrewCloud.Vet.Application.Features.Appointments.Queries;

namespace BrewCloud.Vet.Api.Controllers
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

        [HttpPost(Name = "UpdateAppointmentStatus")]
        public async Task<IActionResult> UpdateAppointmentStatus([FromBody] UpdateAppointmentStatusCommand command)
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
         
        [HttpGet(Name = "GetAppointmentDailyList")]
        public async Task<IActionResult> GetAppointmentDailyList()
        {
            var command = new GetAppointmentDailyListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetAppointmentListByPatientId")]
        public async Task<IActionResult> GetAppointmentListByPatientId([FromBody] GetAppointmentListByPatientIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "AppointmentDateCheckControl")]
        public async Task<IActionResult> AppointmentDateCheckControl([FromBody] AppointmentDateCheckControlQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
