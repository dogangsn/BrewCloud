using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR; 
using VetSystems.Vet.Application.Features.Vaccine.Queries;
using VetSystems.Vet.Application.Features.VaccineCalendar.Commands;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VaccineCalendarController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VaccineCalendarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateVaccineExamination")]
        public async Task<IActionResult> CreateVaccineExamination([FromBody] CreateVaccineExaminationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "PatientVaccineList")]
        public async Task<IActionResult> PatientVaccineList([FromBody] PatientVaccineListQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[HttpPost(Name = "UpdateVaccine")]
        //public async Task<IActionResult> UpdateVaccine([FromBody] UpdateVaccineCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpPost(Name = "DeteleVaccine")]
        //public async Task<IActionResult> DeteleVaccine([FromBody] DeteleVaccineCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

    }
}
