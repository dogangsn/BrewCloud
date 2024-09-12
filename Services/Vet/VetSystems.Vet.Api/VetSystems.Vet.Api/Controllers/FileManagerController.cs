using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Definition.Taxis.Commands;
using VetSystems.Vet.Application.Features.Definition.Taxis.Queries;
using VetSystems.Vet.Application.Features.FileManager.Commands;
using VetSystems.Vet.Application.Features.FileManager.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FileManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region FileManager

        [HttpGet(Name = "GetFileManagerList")]
        public async Task<IActionResult> GetFileManagerList()
        {
            var command = new GetFileManagerListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateFileManager")]
        public async Task<IActionResult> CreateFileManager([FromForm] CreateFileManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "DeleteFileManager")]
        public async Task<IActionResult> DeleteFileManager([FromBody] DeleteFileManagerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPost(Name = "GetFileManagerForById")]
        public async Task<IActionResult> GetFileManagerForById([FromBody] GetFileManagerForByIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DownloadFileManager")]
        public async Task<IActionResult> DownloadFileManager([FromBody] DownloadFileManagerCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccessful)
            {
                return BadRequest(result.Errors);
            }
            return File(result.Data, "", "");
        }



        #endregion


    }
}
