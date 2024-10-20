﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Agenda.Commands;
using BrewCloud.Vet.Application.Features.Agenda.Queries;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AgendaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "AgendaList")]
        public async Task<IActionResult> AgendaList()
        {
            var command = new AgendaListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost(Name = "AgendaListById")]
        public async Task<IActionResult> AgendaListById([FromBody] AgendaListByIdQuery command)
        {
            //var command = new AgendaListByIdQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateAgenda")]
        public async Task<IActionResult> CreateAgenda([FromBody] CreateAgendaCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateAgenda")]
        public async Task<IActionResult> UpdateAgenda([FromBody] UpdateAgendaCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteAgenda")]
        public async Task<IActionResult> DeleteAgenda([FromBody] DeleteAgendaCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
