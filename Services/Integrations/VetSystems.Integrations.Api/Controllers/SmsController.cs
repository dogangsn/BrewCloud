using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Integrations.Application.Features.Integrations.SsmTransactions.Commands;

namespace VetSystems.Integrations.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "SendSmsTransactions")]
        public async Task<IActionResult> SendSmsTransactions([FromBody] SendSmsTransactionsCommand query)
        {
            return Ok(await _mediator.Send(query));
        }


    }
}
