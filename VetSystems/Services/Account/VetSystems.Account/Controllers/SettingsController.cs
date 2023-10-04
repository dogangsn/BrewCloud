using MediatR;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Account.Application.Features.Settings.Queries;

namespace VetSystems.Account.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet(Name = "GetUsersList")]
        public async Task<IActionResult> GetUsersList()
        {
            var command = new GetUsersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
