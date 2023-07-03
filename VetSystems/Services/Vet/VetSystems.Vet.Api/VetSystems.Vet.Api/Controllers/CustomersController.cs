using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Account.Commands;
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
        public async Task<IActionResult> CustomersList(CustomersListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
