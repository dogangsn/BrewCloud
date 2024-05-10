using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.PetHotels.Rooms.Commands;
using VetSystems.Vet.Application.Features.PetHotels.Rooms.Queries;
using VetSystems.Vet.Application.Features.Vaccine.Commands;

namespace VetSystems.Vet.Api.Controllers
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
         
        #endregion
    }
}
