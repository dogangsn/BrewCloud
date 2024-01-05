using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Customers.Queries;
using VetSystems.Vet.Application.Features.Dashboards.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetDashBoard")]
        public async Task<IActionResult> GetDashBoard()
        {
            var command = new GetDashBoardQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
