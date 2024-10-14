using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR; 
using BrewCloud.Vet.Application.Features.Vaccine.Queries;
using BrewCloud.Vet.Application.Features.VaccineCalendar.Commands;
using BrewCloud.Vet.Application.Features.Vaccine.Commands;

namespace BrewCloud.Vet.Api.Controllers
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

        [HttpPost(Name = "UpdateVaccineExamination")]
        public async Task<IActionResult> UpdateVaccineExamination([FromBody] UpdateVaccineExaminationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeteleVaccineAppointment")]
        public async Task<IActionResult> DeteleVaccineAppointment([FromBody] DeteleVaccineAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet(Name = "AllVaccineAppointmentsList")]
        public async Task<IActionResult> AllVaccineAppointmentsList()
        {
            var command = new AllVaccineAppointmentsListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
