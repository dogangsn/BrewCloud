using MediatR;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Clinicalstatistics.Queries;
using VetSystems.Vet.Application.Features.Reports.Commands;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost(Name = "CreateReportFilter")]
        public async Task<IActionResult> CreateReportFilter([FromBody] CreateReportFilterCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
    // [Route("api/[controller]/[action]")]
    //[ApiController]
    //public class ClinicalstatisticsController : ControllerBase
    //{
    //    private readonly IMediator _mediator;
    //    public ClinicalstatisticsController(IMediator mediator)
    //    {
    //        _mediator = mediator;
    //    }

    //}
