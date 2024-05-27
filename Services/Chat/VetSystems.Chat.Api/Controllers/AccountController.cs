using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Chat.Application.Features.Account.Queries;

namespace VetSystems.Chat.Api.Controllers
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

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var command = new GetAllUsersQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
