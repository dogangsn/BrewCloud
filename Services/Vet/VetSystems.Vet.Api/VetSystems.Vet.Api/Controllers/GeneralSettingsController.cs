using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries;
using VetSystems.Vet.Application.Features.GeneralSettings.Users.Queries;

namespace VetSystems.Vet.Api.Controllers
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


    }
}
