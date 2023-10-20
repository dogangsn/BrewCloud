using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Agenda.Commands;
using VetSystems.Vet.Application.Features.Agenda.Queries;
using VetSystems.Vet.Application.Features.Demands.DemandProducts.Commands;
using VetSystems.Vet.Application.Features.Demands.DemandProducts.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DemandsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "DemandProductsList")]
        public async Task<IActionResult> DemandProductsList()
        {
            var command = new DemandProductsListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateDemandProducts")]
        public async Task<IActionResult> CreateDemandProducts([FromBody] CreateDemandProductsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateDemandProducts")]
        public async Task<IActionResult> UpdateDemandProducts([FromBody] UpdateDemandProductsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteDemandProducts")]
        public async Task<IActionResult> DeleteDemandProducts([FromBody] DeleteDemandProductsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
