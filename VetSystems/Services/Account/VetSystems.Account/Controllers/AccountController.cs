using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Account.Application.Operations.Accounts.Commands;

namespace VetSystems.Account.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(Name = "CreateDatabase")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDatabase([FromBody] CreateCustomerDatabaseCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
