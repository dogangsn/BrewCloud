using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Clinicalstatistics.Queries;
using VetSystems.Vet.Application.Features.SaleBuy.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClinicalstatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClinicalstatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "ClinicalstatisticsList")]
        public async Task<IActionResult> GetClinicalstatisticsList([FromBody] ClinicalstatisticsListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost(Name = "GraphicList")]
        public async Task<IActionResult> GetGraphicList([FromBody] GraphicListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
