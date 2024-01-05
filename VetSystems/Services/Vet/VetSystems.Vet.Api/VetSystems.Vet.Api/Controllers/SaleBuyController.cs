using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;
using VetSystems.Vet.Application.Features.SaleBuy.Commands;
using VetSystems.Vet.Application.Features.SaleBuy.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleBuyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SaleBuyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "SaleBuyList")]
        public async Task<IActionResult> SaleBuyList([FromBody] SaleBuyListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateSaleBuy")]
        public async Task<IActionResult> CreateSaleBuy([FromBody] CreateSaleBuyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteSaleBuy")]
        public async Task<IActionResult> DeleteSaleBuy([FromBody] DeleteSaleBuyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "SaleBuyListFilter")]
        public async Task<IActionResult> SaleBuyListFilter([FromBody] SaleBuyListFilterQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateSaleBuy")]
        public async Task<IActionResult> UpdateSaleBuy([FromBody] UpdateSaleBuyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
