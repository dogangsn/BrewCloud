using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Mail.Application.Features.SmtpMails.Commands;
using VetSystems.Mail.Application.Features.SmtpSettings.Commands;
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
         
        [HttpPost(Name = "CreateSmtpSetting")]
        public async Task<IActionResult> CreateSmtpSetting(CreateSmtpSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateSmtpSetting")]
        public async Task<IActionResult> UpdateSmtpSetting(UpdateSmtpSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteSmtpSetting")]
        public async Task<IActionResult> DeleteSmtpSetting(DeleteSmtpSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "SendMail")]
        public async Task<IActionResult> SendMail([FromBody] SendMailCommand query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
