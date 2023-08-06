using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Customers.Queries;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefinitionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DefinitionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "ProductDescriptionList")]
        public async Task<IActionResult> ProductDescriptionList()
        {
            var command = new ProductDescriptionListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }





    }
}
