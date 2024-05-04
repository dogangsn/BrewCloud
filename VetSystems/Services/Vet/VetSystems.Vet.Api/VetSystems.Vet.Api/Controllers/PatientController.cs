using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.GeneralSettings.Users.Queries;
using VetSystems.Vet.Application.Features.Patient.PatientList.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region PatientList

        [HttpGet(Name = "GetPatientList")]
        public async Task<IActionResult> GetPatientList()
        {
            var command = new GetPatientListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion


    }
}
