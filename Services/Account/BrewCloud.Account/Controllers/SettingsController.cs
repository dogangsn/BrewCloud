﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Account.Application.Features.Settings.Queries;
using BrewCloud.Account.Application.Features.Settings.Commands;

namespace BrewCloud.Account.Api.Controllers
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


        [HttpPost(Name = "GetRoleSettingById")]
        public async Task<IActionResult> GetRoleSettingById([FromBody] GetRoleSettingByIdQuery command)
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

        [HttpPost(Name = "CreateBranch")]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateBranch")]
        public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteBranch")]
        public async Task<IActionResult> DeleteBranch([FromBody] DeleteBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetBranchList")]
        public async Task<IActionResult> GetBranchList()
        {
            var query = new GetBranchListQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
