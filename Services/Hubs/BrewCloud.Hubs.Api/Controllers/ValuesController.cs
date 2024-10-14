using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BrewCloud.Hubs.Api.Features;

namespace BrewCloud.Hubs.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "RefreshAppointmentCalendar")]
        public async Task<IActionResult> RefreshAppointmentCalendar([FromBody] RefreshAppointmentCalendarCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
