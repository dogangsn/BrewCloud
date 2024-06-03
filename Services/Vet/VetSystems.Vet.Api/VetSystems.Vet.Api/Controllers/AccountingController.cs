using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using VetSystems.Vet.Application.Features.Agenda.Commands;
using VetSystems.Vet.Application.Features.Accounting.Commands;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateSale")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
