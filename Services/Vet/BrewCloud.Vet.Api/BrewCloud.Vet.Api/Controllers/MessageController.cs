using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Appointment.Commands;
using BrewCloud.Vet.Application.Features.Message.Commands;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }
         
        [HttpPost(Name = "MultiAutoSendMessage")]
        public async Task<IActionResult> MultiAutoSendMessage([FromBody] MultiAutoSendMessageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
