using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.PetHotels.Accomodation.Commands;
using BrewCloud.Vet.Application.Features.PetHotels.Accomodation.Queries;
using BrewCloud.Vet.Application.Features.PetHotels.Rooms.Commands;
using BrewCloud.Vet.Application.Features.PetHotels.Rooms.Queries;
using BrewCloud.Vet.Application.Features.Vaccine.Commands;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PetHotelsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PetHotelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Rooms

        [HttpGet(Name = "GetRoomList")]
        public async Task<IActionResult> GetRoomList()
        {
            var command = new GetRoomListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateRoom")]
        public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteRoom")]
        public async Task<IActionResult> DeleteRoom([FromBody] DeleteRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion

        #region Accomodation

         
        [HttpPost(Name = "GetAccomodationList")]
        public async Task<IActionResult> GetAccomodationList([FromBody] GetAccomodationListQuery command)
        { 
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateAccomodation")]
        public async Task<IActionResult> CreateAccomodation([FromBody] CreateAccomodationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateAccomodation")]
        public async Task<IActionResult> UpdateAccomodation([FromBody] UpdateAccomodationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteAccomodation")]
        public async Task<IActionResult> DeleteAccomodation([FromBody] DeleteAccomodationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateCheckOut")]
        public async Task<IActionResult> UpdateCheckOut([FromBody] UpdateCheckOutCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion
    }
}
