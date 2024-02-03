using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries;
using VetSystems.Vet.Application.Features.SaleBuy.Commands;
using VetSystems.Vet.Application.Features.Settings.Parameters.Commands;
using VetSystems.Vet.Application.Features.Settings.Parameters.Queries;
using VetSystems.Vet.Application.Features.Settings.SmsParameters.Commands;

namespace VetSystems.Vet.Api.Controllers
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



        #endregion
    }
}
