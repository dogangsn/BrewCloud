using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Mail.Application.Features.SmtpSettings.Queries;

namespace VetSystems.Mail.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MailingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetSmtpSettings")]
        public async Task<IActionResult> GetSmtpSettings()
        {
            var command = new GetSmtpSettingsQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
