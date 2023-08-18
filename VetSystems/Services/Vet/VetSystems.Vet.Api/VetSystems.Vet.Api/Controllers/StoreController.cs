using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Definition.ProductCategory.Commands;
using VetSystems.Vet.Application.Features.Store.Commands;
using VetSystems.Vet.Application.Features.Store.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "StoreList")]
        public async Task<IActionResult> StoreList()
        {
            var command = new StoreListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateStore")]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateStore")]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteStore")]
        public async Task<IActionResult> DeleteStore([FromBody] DeleteStoreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
