using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Account.Commands;
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Application.Features.Customers.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "CustomersList")]
        public async Task<IActionResult> CustomersList()
        {
            var command = new CustomersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "AnimalBreedsDefList")]
        public async Task<IActionResult> AnimalBreedsDefList()
        {
            var command = new AnimalBreedsDefListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "VetAnimalsTypeList")]
        public async Task<IActionResult> VetAnimalsTypeList()
        {
            var command = new VetVetAnimalsTypeListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
