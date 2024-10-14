using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BrewCloud.IdentityServer.Application.Features.Accounts.Commands;
using BrewCloud.Shared.Service;

namespace BrewCloud.IdentityServer.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityRepository _identityRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IMediator mediator, IIdentityRepository identityRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _identityRepository = identityRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost(Name = "Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateTempCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost(Name = "ComplateActivation")]
        [AllowAnonymous]
        public async Task<IActionResult> ComplateActivation([FromBody] ComplateActivationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "ComplateSubscription")]
        [AllowAnonymous]
        public async Task<IActionResult> ComplateSubscription([FromBody] ComplateSubscriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "RefreshActivation")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshActivation([FromBody] RefreshActivationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
