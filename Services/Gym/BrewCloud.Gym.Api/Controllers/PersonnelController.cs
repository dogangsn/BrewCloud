using BrewCloud.Gym.Application.Features.Personnel.Commands;
using BrewCloud.Gym.Application.Features.Personnel.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrewCloud.Gym.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonnelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateGymPersonnel")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateGymPersonnel([FromBody] CreateGymPersonnelCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateGymPersonnel")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateGymPersonnel([FromBody] UpdateGymPersonnelCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteGymPersonnel")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteGymPersonnel([FromBody] DeleteGymPersonnelCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetGymPersonnelList")]
        public async Task<IActionResult> GetGymPersonnelList()
        {
            var query = new GetGymPersonnelListQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
