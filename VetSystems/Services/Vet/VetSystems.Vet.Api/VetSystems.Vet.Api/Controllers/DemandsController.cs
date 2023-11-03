using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Agenda.Commands;
using VetSystems.Vet.Application.Features.Agenda.Queries;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries;
using VetSystems.Vet.Application.Features.Demands.Demand.Commands;
using VetSystems.Vet.Application.Features.Demands.Demand.Queries;
using VetSystems.Vet.Application.Features.Demands.DemandComplated.Commands.Queries;
using VetSystems.Vet.Application.Features.Demands.DemandProducts.Commands;
using VetSystems.Vet.Application.Features.Demands.DemandProducts.Queries;
using VetSystems.Vet.Application.Features.Demands.DemandTrans.Queries;

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
        #region DemandProducts
        
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
        #endregion
        #region Demands
        [HttpGet(Name = "DemandList")]
        public async Task<IActionResult> DemandList()
        {
            var command = new DemandListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateDemand")]
        public async Task<IActionResult> CreateDemand([FromBody] CreateDemandCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateDemand")]
        public async Task<IActionResult> UpdateDemand([FromBody] UpdateDemandCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteDemand")]
        public async Task<IActionResult> DeleteDemand([FromBody] DeleteDemandCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
        #region DemandTrans
        [HttpPost(Name = "DemandTransList")]
        public async Task<IActionResult> DemandTransList([FromBody] DemandTransListQuery command)
        {
            //var command = new DemandTransListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
        #region DemandComplated
        [HttpGet(Name = "DemandComplatedList")]
        public async Task<IActionResult> DemandComplatedList()
        {
            var command = new DemandComplatedListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        #endregion
    }
}
