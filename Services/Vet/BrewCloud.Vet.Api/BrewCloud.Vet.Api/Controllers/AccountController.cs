using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Account.Commands;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "UpdateDatabase")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateDatabase([FromBody] UpdateDatabaseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
