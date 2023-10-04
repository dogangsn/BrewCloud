using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Agenda.Commands;
using VetSystems.Vet.Application.Features.Agenda.Queries;

namespace VetSystems.Vet.Api.Controllers
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
