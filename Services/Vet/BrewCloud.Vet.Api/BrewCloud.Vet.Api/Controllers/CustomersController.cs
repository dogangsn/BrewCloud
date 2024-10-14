using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Account.Commands;
using BrewCloud.Vet.Application.Features.Customers.Commands;
using BrewCloud.Vet.Application.Features.Customers.Queries;

namespace BrewCloud.Vet.Api.Controllers
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

        [HttpPost(Name = "CustomersList")]
        public async Task<IActionResult> CustomersList([FromBody] CustomersListQuery query)
        { 
            var result = await _mediator.Send(query);
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
         
        [HttpPost(Name = "UpdatePatient")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientCommand command)
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

        [HttpPost(Name = "SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetSalesCustomerList")]
        public async Task<IActionResult> GetSalesCustomerList([FromBody] GetSalesCustomerListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "UpdateCustomerArchive")]
        public async Task<IActionResult> UpdateCustomerArchive([FromBody] UpdateCustomerArchiveCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetFarmCustomersList")]
        public async Task<IActionResult> GetFarmCustomersList()
        {
            var command = new GetFarmCustomersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
