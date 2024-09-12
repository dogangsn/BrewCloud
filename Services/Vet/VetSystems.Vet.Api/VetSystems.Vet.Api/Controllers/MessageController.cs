using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Appointment.Commands;
using VetSystems.Vet.Application.Features.Message.Commands;

namespace VetSystems.Vet.Api.Controllers
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
