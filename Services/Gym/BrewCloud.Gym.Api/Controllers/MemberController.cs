using BrewCloud.Gym.Application.Features.Member.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrewCloud.Gym.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateGymMember")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateGymMember([FromBody] CreateGymMemberCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
