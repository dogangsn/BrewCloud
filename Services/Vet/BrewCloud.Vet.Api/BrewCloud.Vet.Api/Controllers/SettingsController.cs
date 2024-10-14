using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Definition.ProductDescription.Commands;
using BrewCloud.Vet.Application.Features.Definition.ProductDescription.Queries;
using BrewCloud.Vet.Application.Features.SaleBuy.Commands;
using BrewCloud.Vet.Application.Features.Settings.Parameters.Commands;
using BrewCloud.Vet.Application.Features.Settings.Parameters.Queries;
using BrewCloud.Vet.Application.Features.Settings.SmsParameters.Commands;
using BrewCloud.Vet.Application.Features.Settings.SmsParameters.Queries;
using BrewCloud.Vet.Application.Features.Settings.VetLog.Queries;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Parameters 

        [HttpGet(Name = "ParametersList")]
        public async Task<IActionResult> ParametersList()
        {
            var command = new ParametersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateParameters")]
        public async Task<IActionResult> UpdateParameters([FromBody] UpdateParametersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion

        #region SmsParameters

        [HttpPost(Name = "CreateSmsParameters")]
        public async Task<IActionResult> CreateSmsParameters([FromBody] CreateSmsParametersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateSmsParameters")]
        public async Task<IActionResult> UpdateSmsParameters([FromBody] UpdateSmsParametersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetSmsParametersIdBy")]
        public async Task<IActionResult> GetSmsParametersIdBy([FromBody] GetSmsParametersIdByQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpGet(Name = "GetSmsParametersList")]
        public async Task<IActionResult> GetSmsParametersList()
        {
            var command = new GetSmsParametersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion

        #region Logs


        [HttpPost(Name = "GetLogs")]
        public async Task<IActionResult> GetLogs([FromBody] GetLogsQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

    }
}
