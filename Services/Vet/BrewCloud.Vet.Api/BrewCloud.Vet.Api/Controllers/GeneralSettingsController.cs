using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Appointment.Commands;
using BrewCloud.Vet.Application.Features.Definition.ProductDescription.Queries;
using BrewCloud.Vet.Application.Features.GeneralSettings.Users.Queries;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeneralSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GeneralSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetVetUsersList")]
        public async Task<IActionResult> GetVetUsersList()
        {
            var command = new GetVetUsersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetShortCuts")]
        public async Task<IActionResult> GetShortCuts()
        {
            var command = new GetShortCutsQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost(Name = "CreateShortCuts")]
        public async Task<IActionResult> CreateShortCuts([FromBody] CreateShortCutsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteShortCuts")]
        public async Task<IActionResult> DeleteShortCuts([FromBody] DeleteShortCutsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateShortCuts")]
        public async Task<IActionResult> UpdateShortCuts([FromBody] UpdateShortCutsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
