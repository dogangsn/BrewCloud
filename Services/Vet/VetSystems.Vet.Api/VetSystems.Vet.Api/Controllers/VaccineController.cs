using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR; 
using VetSystems.Vet.Application.Features.Vaccine.Queries;
using VetSystems.Vet.Application.Features.Vaccine.Commands;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VaccineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "VaccineList")]
        public async Task<IActionResult> VaccineList([FromBody] VaccineListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateVaccine")]
        public async Task<IActionResult> CreateVaccine([FromBody] CreateVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateVaccine")]
        public async Task<IActionResult> UpdateVaccine([FromBody] UpdateVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeteleVaccine")]
        public async Task<IActionResult> DeteleVaccine([FromBody] DeteleVaccineCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
