using MediatR;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Suppliers.Queries;
using VetSystems.Vet.Application.Features.Store.Commands;
using VetSystems.Vet.Application.Features.Store.Queries;
using VetSystems.Vet.Application.Features.Suppliers.Commands;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "SuppliersList")]
        public async Task<IActionResult> SuppliersList()
        {
            var command = new SuppliersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateSuppliers")]
        public async Task<IActionResult> CreateSuppliers([FromBody] CreateSuppliersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateSuppliers")]
        public async Task<IActionResult> UpdateSuppliers([FromBody] UpdateSuppliersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteSuppliers")]
        public async Task<IActionResult> DeleteSuppliers([FromBody] DeleteSuppliersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
