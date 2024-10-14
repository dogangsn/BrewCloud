using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BrewCloud.Vet.Application.Features.Agenda.Commands;
using BrewCloud.Vet.Application.Features.Accounting.Commands;
using BrewCloud.Vet.Application.Features.Accounting.Queries;

namespace BrewCloud.Vet.Api.Controllers
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

        [HttpPost(Name = "UpdateSale")]
        public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateSaleCollection")]
        public async Task<IActionResult> CreateSaleCollection([FromBody] CreateSaleCollectionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "DeleteCollection")]
        public async Task<IActionResult> DeleteCollection([FromBody] DeleteCollectionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetSalesById")]
        public async Task<IActionResult> GetSalesById([FromBody] GetSalesByIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "CreateBalanceSaleCollection")]
        public async Task<IActionResult> CreateBalanceSaleCollection([FromBody] CreateBalanceSaleCollectionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "IsSaleProductControl")]
        public async Task<IActionResult> IsSaleProductControl([FromBody] IsSaleProductControlQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateSaleCollection")]
        public async Task<IActionResult> UpdateSaleCollection([FromBody] UpdateSaleCollectionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
