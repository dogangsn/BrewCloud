using MediatR;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Account.Application.Features.Settings.Queries;
using VetSystems.Account.Application.Features.Settings.Commands;

namespace VetSystems.Account.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet(Name = "GetUsersList")]
        public async Task<IActionResult> GetUsersList()
        {
            var command = new GetUsersListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetCompany")]
        public async Task<IActionResult> GetCompany()
        {
            var command = new GetCompanyQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost(Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateRoleSetting")]
        public async Task<IActionResult> CreateRoleSetting([FromBody] CreateRoleSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetRoleSettingList")]
        public async Task<IActionResult> GetRoleSettingList()
        {
            var command = new GetRoleSettingListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetUserRoleSettingList")]
        public async Task<IActionResult> GetUserRoleSettingList()
        {
            var command = new GetUserRoleSettingListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteRoleSetting")]
        public async Task<IActionResult> DeleteRoleSetting([FromBody] DeleteRoleSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateRoleSetting")]
        public async Task<IActionResult> UpdateRoleSetting([FromBody] UpdateRoleSettingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost(Name = "GetNavigation")]
        public async Task<IActionResult> GetNavigation([FromBody] GetNavigationQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetTitleDefination")]
        public async Task<IActionResult> GetTitleDefination()
        {
            var command = new GetTitleDefinationQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateTitleDefination")]
        public async Task<IActionResult> CreateTitleDefination([FromBody] CreateTitleDefinationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateTitleDefination")]
        public async Task<IActionResult> UpdateTitleDefination([FromBody] UpdateTitleDefinationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteTileDefination")]
        public async Task<IActionResult> DeleteTileDefination([FromBody] DeleteTileDefinationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpGet(Name = "GetActiveUser")]
        public async Task<IActionResult> GetActiveUser()
        {
            var command = new GetActiveUserQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
