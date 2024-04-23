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

        [HttpPost(Name = "GetCustomersFindById")]
        public async Task<IActionResult> GetCustomersFindById([FromBody] CustomersFindByIdQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost(Name = "GetPatientsByCustomerId")]
        public async Task<IActionResult> GetPatientsByCustomerId([FromBody] GetPatientsByCustomerIdQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateCustomerById")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCustomerById([FromBody] UpdateCustomerCommand model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost(Name = "CreatePatient")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeletePatient")]
        public async Task<IActionResult> DeletePatient([FromBody] DeletePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetTransactionMovementList")]
        public async Task<IActionResult> GetTransactionMovementList([FromBody] GetTransactionMovementListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "GetPaymentTransactionList")]
        public async Task<IActionResult> GetPaymentTransactionList([FromBody] GetPaymentTransactionListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateCollection")]
        public async Task<IActionResult> CreateCollection([FromBody] CreateCollectionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetPayChartList")]
        public async Task<IActionResult> GetPayChartList([FromBody] GetPayChartListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeletePayChart")]
        public async Task<IActionResult> DeletePayChart([FromBody] DeletePayChartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
