using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Account.Application.Features.Account.Commands;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityRepository _identityRepository;
        public AccountController(IMediator mediator, IIdentityRepository identityRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityRepository = identityRepository;
        }

        [HttpPost(Name = "ComplateSubscription")]
        [AllowAnonymous]
        public async Task<IActionResult> ComplateSubscription([FromBody] ComplateSubscriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateSubscription")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
